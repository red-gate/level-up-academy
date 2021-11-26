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

        public event Action? OnLicencesAllocated;

        public LicenseAllocator(IBaseMonitorRegistry baseMonitorRegistry, ILicenseService licenseService, ServerRetriever serverRetriever)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
            _licenseService = licenseService;
            _serverRetriever = serverRetriever;
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

            // Rank them by the oldest first, as we give licensing preference to longer lived servers over newly
            // registered servers.
            var rankedServers = allServers
                .SelectMany(pair => pair.Value.Select(server => (BaseMonitorName: pair.Key, Server: server)))
                .OrderBy(x => x.Server.Added);

            // Decide which servers will and won't be licenced.
            var availableLicenseCount = await availableLicenseCountTask;
            Console.WriteLine($"Available license count: {availableLicenseCount}");

            var licensedServers = new List<(string BaseMonitorName, Server Server)>();
            var unlicensedServers = new List<(string BaseMonitorName, Server Server)>();
            foreach (var item in rankedServers)
            {
                // If the server is suspended, or we've run out of licences, then the server won't be licensed.
                if (item.Server.IsSuspended || availableLicenseCount == 0)
                {
                    unlicensedServers.Add(item);
                }
                // Otherwise it will. Woot!
                else
                {
                    licensedServers.Add(item);
                    availableLicenseCount--;
                }
            }

            // Mutate the server license state where necessary.
            var newlyLicensedServers = licensedServers
                .Where(x => !x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with {IsLicensed = true}));
            var newlyUnlicensedServers = unlicensedServers
                .Where(x => x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with {IsLicensed = false}));
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
