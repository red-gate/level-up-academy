using System.Collections.Generic;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface ILicenseAlgorithm
    {
        (IReadOnlyList<MonitoredServer> licensedServers, IReadOnlyList<MonitoredServer> unlicensedServers) CalculateLicensedAndUnlicensedServers(
            IDictionary<string, IEnumerable<Server>> allServers, int availableLicenseCount);
    }
}