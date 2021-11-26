using System.Collections.Generic;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class ServerRetriever
    {
        private readonly IBaseMonitorRegistry _baseMonitorRegistry;

        public ServerRetriever(IBaseMonitorRegistry baseMonitorRegistry)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
        }

        public async Task<IDictionary<string, IEnumerable<Server>>> GetAllServers()
        {
            // Fetch all the servers from all of the base monitors.
            var allServers = await _baseMonitorRegistry.ExecuteOnAllBaseMonitorsAsync(baseMonitor =>
                baseMonitor.MonitoredServersRepository.GetAllServers());
            return allServers;
        }
    }
}