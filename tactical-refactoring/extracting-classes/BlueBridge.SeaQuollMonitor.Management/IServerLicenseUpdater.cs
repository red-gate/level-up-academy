using System.Collections.Generic;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface IServerLicenseUpdater
    {
        Task UpdateServerLicenses(IReadOnlyList<MonitoredServer> licensedServers, IReadOnlyList<MonitoredServer> unlicensedServers);
    }
}