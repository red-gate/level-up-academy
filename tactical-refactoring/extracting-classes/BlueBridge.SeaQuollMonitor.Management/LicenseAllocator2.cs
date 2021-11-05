using System.Collections.Generic;
using System.Linq;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class LicenseAllocator2 : ILicenseAllocator2
    {
        public ILookup<string, Server> ServersWithChangedLicenseState(ServerLicenseAllocations allocations)
        {
            // Mutate the server license state where necessary.
            var newlyLicensedServers = allocations.Licensed
                .Where(x => !x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with {IsLicensed = true}));
            var newlyUnlicensedServers = allocations.Unlicensed
                .Where(x => x.Server.IsLicensed)
                .Select(x => (BaseMonitorName: x.BaseMonitorName, Server: x.Server with {IsLicensed = false}));
            var modifiedServers = newlyLicensedServers
                .Concat(newlyUnlicensedServers)
                .ToLookup(x => x.BaseMonitorName, x => x.Server);
            return modifiedServers;
        }

        public ServerLicenseAllocations AllocateLicenses(
            IEnumerable<ServerWithBaseMonitorName> rankedServers,
            int availableLicenseCount)
        {
            var licensedServers = new List<ServerWithBaseMonitorName>();
            var unlicensedServers = new List<ServerWithBaseMonitorName>();
            foreach (var item in rankedServers)
            {
                // If the server is suspended, or we've run out of licences, then the server won't be licensed.
                if (item.Server.IsSuspended || availableLicenseCount == 0)
                {
                    unlicensedServers.Add(item);
                }
                // Otherwise it will. Woot!
                else
                {
                    licensedServers.Add(item);
                    availableLicenseCount--;
                }
            }

            return new ServerLicenseAllocations(licensedServers, unlicensedServers);
        }
    }

    public interface ILicenseAllocator2
    {
        ILookup<string, Server> ServersWithChangedLicenseState(ServerLicenseAllocations allocations);

        ServerLicenseAllocations AllocateLicenses(
            IEnumerable<ServerWithBaseMonitorName> rankedServers,
            int availableLicenseCount);
    }
}