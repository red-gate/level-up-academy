using System.Collections.Generic;
using System.Linq;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public delegate (List<(string BaseMonitorName, Server Server)> licensedServers,
        List<(string BaseMonitorName, Server Server)> unlicensedServers) LicenseAllocationAlgorithm(
            IDictionary<string, IEnumerable<Server>> allServers, int availableLicenseCount);
    internal static class DefaultLicenseAllocationAlgorithm
    {
        public static
            (List<(string BaseMonitorName, Server Server)> licensedServers,
            List<(string BaseMonitorName, Server Server)> unlicensedServers) CalculateLicensedServers(
                IDictionary<string, IEnumerable<Server>> allServers, int availableLicenseCount)
        {
            // Rank them by the oldest first, as we give licensing preference to longer lived servers over newly
            // registered servers.
            var rankedServers = allServers
                .SelectMany(pair => pair.Value.Select(server => (BaseMonitorName: pair.Key, Server: server)))
                .OrderBy(x => x.Server.Added);

            System.Console.WriteLine($"Available license count: {availableLicenseCount}");

            var licensedServers = new List<(string BaseMonitorName, Server Server)>();
            var unlicensedServers = new List<(string BaseMonitorName, Server Server)>();
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

            return (licensedServers, unlicensedServers);
        }
    }
}