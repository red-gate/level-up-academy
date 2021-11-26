using System;
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
        private readonly TaskDebouncer _refreshTaskDebouncer;
        private readonly ServerRetriever _serverRetriever;
        private readonly LicenseAlgorithm _licenseAlgorithm;

        public event Action? OnLicencesAllocated;

        public LicenseAllocator(IBaseMonitorRegistry baseMonitorRegistry, ILicenseService licenseService,
            ServerRetriever serverRetriever, LicenseAlgorithm licenseAlgorithm)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
            _licenseService = licenseService;
            _serverRetriever = serverRetriever;
            _licenseAlgorithm = licenseAlgorithm;
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

            var allServers = await _serverRetriever.GetAllServers();

            var (licensedServers, unlicensedServers) = _licenseAlgorithm.CalculateLicensedAndUnlicensedServers(allServers, await availableLicenseCountTask);

            // Mutate the server license state where necessary.
            var newlyLicensedServers = licensedServers
                .Where(x => !x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with { IsLicensed = true }));
            var newlyUnlicensedServers = unlicensedServers
                .Where(x => x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with { IsLicensed = false }));
            var modifiedServers = newlyLicensedServers
                .Concat(newlyUnlicensedServers)
                .ToLookup(x => x.BaseMonitorName, x => x.Server);

            // Now update the modified servers.
            await _baseMonitorRegistry.ExecuteOnAllBaseMonitorsAsync(async baseMonitor =>
            {
                foreach (var server in modifiedServers[baseMonitor.Name])
                {
                    await baseMonitor.MonitoredServersRepository.UpdateServer(server);
                }
            });

            // Report the number of licenses consumed.
            Console.WriteLine($"Used license count: {licensedServers.Count}");
            await _licenseService.ReportUsedLicenseCount(licensedServers.Count);

            // And finally fire the licenses allocated event.
            OnLicencesAllocated?.Invoke();
        }

        protected override void OnDispose()
        {
            _baseMonitorRegistry.OnLicensingRequirementsChanged -= HandleLicensingRequirementsChanged;
            _licenseService.OnAvailableLicensesChanged -= HandleLicensingRequirementsChanged;
            _refreshTaskDebouncer.Dispose();
        }
    }
}