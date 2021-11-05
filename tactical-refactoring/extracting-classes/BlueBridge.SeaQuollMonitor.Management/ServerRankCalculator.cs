using System.Collections.Generic;
using System.Linq;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class ServerRankCalculator : IServerRankCalculator
    {
        public IEnumerable<ServerWithBaseMonitorName> Rank(IDictionary<string, IEnumerable<Server>> allServers)
        {
            // Rank them by the oldest first, as we give licensing preference to longer lived servers over newly
            // registered servers.
            var rankedServers = allServers
                .SelectMany(pair => pair.Value.Select(server => new ServerWithBaseMonitorName(server, pair.Key)))
                .OrderBy(x => x.Server.Added);
            
            return rankedServers;
        }
    }
}