using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;
using BlueBridge.SeaQuollMonitor.Utilities;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class LicenseAllocator : DisposableBase, ILicenseAllocator
    {
        private readonly IBaseMonitorRegistry _baseMonitorRegistry;
        private readonly ILicenseService _licenseService;
        private readonly IServersFetcher _serversFetcher;
        private readonly LicenseAllocationAlgorithm _licenseAllocationAlgorithm;
        private readonly ILicenseUpdater _licenseUpdater;
        private readonly TaskDebouncer _refreshTaskDebouncer;

        public LicenseAllocator(IBaseMonitorRegistry baseMonitorRegistry, ILicenseService licenseService,
            IServersFetcher serversFetcher, LicenseAllocationAlgorithm licenseAllocationAlgorithm,
            ILicenseUpdater licenseUpdater)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
            _licenseService = licenseService;
            _serversFetcher = serversFetcher;
            _licenseAllocationAlgorithm = licenseAllocationAlgorithm;
            _licenseUpdater = licenseUpdater;
            _refreshTaskDebouncer = new TaskDebouncer(DoRefresh);

            _baseMonitorRegistry.OnLicensingRequirementsChanged += HandleLicensingRequirementsChanged;
            _licenseService.OnAvailableLicensesChanged += HandleLicensingRequirementsChanged;
        }

        private void HandleLicensingRequirementsChanged() => Task.Run(() => _refreshTaskDebouncer.Execute()).Wait();

        public Task Refresh() => _refreshTaskDebouncer.Execute();

        private async Task DoRefresh()
        {
            // Fetch the number of available licenses from the licensing service.
            var availableLicenseCountTask = _licenseService.GetAvailableLicenseCount();

            // Fetch all the servers from all of the base monitors.
            var allServers = await _serversFetcher.FetchAllServers();

            var (licensedServers, unlicensedServers) = _licenseAllocationAlgorithm(allServers, await availableLicenseCountTask);

            // Mutate the server license state where necessary.
            await _licenseUpdater.ApplyLicenseChanges(licensedServers, unlicensedServers);

            // And finally report the number of licenses consumed.
            System.Console.WriteLine($"Used license count: {licensedServers.Count}");
            await _licenseService.ReportUsedLicenseCount(licensedServers.Count);
        }

        protected override void OnDispose()
        {
            _baseMonitorRegistry.OnLicensingRequirementsChanged -= HandleLicensingRequirementsChanged;
            _licenseService.OnAvailableLicensesChanged -= HandleLicensingRequirementsChanged;
            _refreshTaskDebouncer.Dispose();
        }
    }
}
