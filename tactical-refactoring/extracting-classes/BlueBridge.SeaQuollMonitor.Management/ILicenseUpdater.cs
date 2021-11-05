using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface ILicenseUpdater
    {
        Task ApplyLicenseChanges(IEnumerable<(string BaseMonitorName, Server Server)> licensedServers, IEnumerable<(string BaseMonitorName, Server Server)> unlicensedServers);
    }

    class LicenseUpdater : ILicenseUpdater
    {

        private readonly IBaseMonitorRegistry _baseMonitorRegistry;

        public LicenseUpdater(
            IBaseMonitorRegistry baseMonitorRegistry)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
        }

        public async Task ApplyLicenseChanges(IEnumerable<(string BaseMonitorName, Server Server)> licensedServers, IEnumerable<(string BaseMonitorName, Server Server)> unlicensedServers)
        {
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
        }

    }
}
