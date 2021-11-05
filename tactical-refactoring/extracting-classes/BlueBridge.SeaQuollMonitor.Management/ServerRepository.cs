using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class ServerRepository : IServerRepository
    {
        private readonly IBaseMonitorRegistry _baseMonitorRegistry;

        public ServerRepository(IBaseMonitorRegistry baseMonitorRegistry)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
        }

        public async Task<IDictionary<string, IEnumerable<Server>>> GetServers()
        {
            // Fetch all the servers from all of the base monitors.
            var allServers = await _baseMonitorRegistry.ExecuteOnAllBaseMonitorsAsync(baseMonitor =>
                baseMonitor.MonitoredServersRepository.GetAllServers());

            return allServers;
        }

        public async Task UpdateLicenseStateForServers(ILookup<string, Server> modifiedServers)
        {
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