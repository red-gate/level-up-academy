using System.Collections.Generic;
using System.Threading.Tasks;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public interface IServersFetcher
    {
        Task<IDictionary<string, IEnumerable<Server>>> FetchAllServers();
    }
}