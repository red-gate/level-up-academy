using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueBridge.SeaQuollMonitor.Domain
{
    public interface IMonitoredServersRepository
    {
        event Action OnLicensingRequirementsChanged;

        Task<IEnumerable<Server>> GetAllServers();

        Task<Server?> GetServer(string name);

        Task CreateServer(Server server);

        Task RemoveServer(string name);

        Task UpdateServer(Server server);
    }
}
