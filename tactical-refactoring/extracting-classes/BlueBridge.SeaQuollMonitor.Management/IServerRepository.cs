using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface IServerRepository
    {
        Task<IDictionary<string, IEnumerable<Server>>> GetServers();
        Task UpdateLicenseStateForServers(ILookup<string, Server> modifiedServers);
    }
}