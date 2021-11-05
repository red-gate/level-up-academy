using System.Collections.Generic;
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
        private readonly ILicenseAllocator2 _licenseAllocator2;
        private readonly IServerRepository _serverRepository;
        private readonly IServerRankCalculator _serverRankCalculator;

        public LicenseAllocator(IBaseMonitorRegistry baseMonitorRegistry, ILicenseService licenseService, ILicenseAllocator2 licenseAllocator2, IServerRepository serverRepository, IServerRankCalculator serverRankCalculator)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
            _licenseService = licenseService;
            _licenseAllocator2 = licenseAllocator2;
            _serverRepository = serverRepository;
            _serverRankCalculator = serverRankCalculator;
            _refreshTaskDebouncer = new TaskDebouncer(DoRefresh);

            _baseMonitorRegistry.OnLicensingRequirementsChanged += HandleLicensingRequirementsChanged;
            _licenseService.OnAvailableLicensesChanged += HandleLicensingRequirementsChanged;
        }

        private void HandleLicensingRequirementsChanged() => Task.Run(() => _refreshTaskDebouncer.Execute()).Wait();

        public Task Refresh() => _refreshTaskDebouncer.Execute();

        private async Task DoRefresh()
        {
            var availableLicenseCountTask = _licenseService.GetAvailableLicenseCount();

            var allServers = await _serverRepository.GetServers();

            var rankedServers = _serverRankCalculator.Rank(allServers);

            var availableLicenseCount = await availableLicenseCountTask;
            System.Console.WriteLine($"Available license count: {availableLicenseCount}");
            var serverLicenseAllocations = _licenseAllocator2.AllocateLicenses(rankedServers, availableLicenseCount);

            var serversWithChangedLicenseState = _licenseAllocator2.ServersWithChangedLicenseState(serverLicenseAllocations);
            await _serverRepository.UpdateLicenseStateForServers(serversWithChangedLicenseState);

            System.Console.WriteLine($"Used license count: {serverLicenseAllocations.Licensed.Count}");
            await _licenseService.ReportUsedLicenseCount(serverLicenseAllocations.Licensed.Count);
        }

        protected override void OnDispose()
        {
            _baseMonitorRegistry.OnLicensingRequirementsChanged -= HandleLicensingRequirementsChanged;
            _licenseService.OnAvailableLicensesChanged -= HandleLicensingRequirementsChanged;
            _refreshTaskDebouncer.Dispose();
        }
    }
    
    public record ServerWithBaseMonitorName (Server Server, string BaseMonitorName);

    public record ServerLicenseAllocations (
        IReadOnlyCollection<ServerWithBaseMonitorName> Licensed,
        IReadOnlyCollection<ServerWithBaseMonitorName> Unlicensed);
}
