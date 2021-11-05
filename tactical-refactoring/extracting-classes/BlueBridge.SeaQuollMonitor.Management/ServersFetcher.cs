using System.Collections.Generic;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    class ServersFetcher : IServersFetcher
    {
        private readonly IBaseMonitorRegistry _baseMonitorRegistry;

        public ServersFetcher(IBaseMonitorRegistry baseMonitorRegistry)
        {
            _baseMonitorRegistry = baseMonitorRegistry;
        }

        public Task<IDictionary<string, IEnumerable<Server>>> FetchAllServers()
        {
            return _baseMonitorRegistry.ExecuteOnAllBaseMonitorsAsync(baseMonitor =>
                baseMonitor.MonitoredServersRepository.GetAllServers());
        }
    }
}