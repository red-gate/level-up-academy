using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class ServerLicenseUpdater : IServerLicenseUpdater
    {
        private readonly IBaseMonitorRegistry _baseMonitorRegistry;

        public ServerLicenseUpdater(IBaseMonitorRegistry baseMonitorRegistry)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
        }

        public async Task UpdateServerLicenses(IReadOnlyList<MonitoredServer> licensedServers, IReadOnlyList<MonitoredServer> unlicensedServers)
        {
            // Mutate the server license state where necessary.
            var modifiedServers = GatherModifiedServers(licensedServers, unlicensedServers);

            // Now update the modified servers.
            await _baseMonitorRegistry.ExecuteOnAllBaseMonitorsAsync(async baseMonitor =>
            {
                foreach (var server in modifiedServers[baseMonitor.Name])
                {
                    await baseMonitor.MonitoredServersRepository.UpdateServer(server);
                }
            });
        }

        private static ILookup<string, Server> GatherModifiedServers(IReadOnlyList<MonitoredServer> licensedServers, IReadOnlyList<MonitoredServer> unlicensedServers)
        {
            var newlyLicensedServers = licensedServers
                .Where(x => !x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with {IsLicensed = true}));
            var newlyUnlicensedServers = unlicensedServers
                .Where(x => x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with {IsLicensed = false}));
            var modifiedServers = newlyLicensedServers
                .Concat(newlyUnlicensedServers)
                .ToLookup(x => x.BaseMonitorName, x => x.Server);
            return modifiedServers;
        }
    }
}