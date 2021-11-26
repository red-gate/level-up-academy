using System.Collections.Generic;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface IServerRetriever
    {
        Task<IDictionary<string, IEnumerable<Server>>> GetAllServers();
    }
}