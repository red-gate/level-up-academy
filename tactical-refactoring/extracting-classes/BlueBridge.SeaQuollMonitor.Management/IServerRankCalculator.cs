using System.Collections.Generic;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface IServerRankCalculator
    {
        IEnumerable<ServerWithBaseMonitorName> Rank(IDictionary<string, IEnumerable<Server>> allServers);
    }
}