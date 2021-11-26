using System;
using System.Collections.Generic;
using System.Linq;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class LicenseAlgorithm : ILicenseAlgorithm
    {
        public (IReadOnlyList<MonitoredServer> licensedServers, IReadOnlyList<MonitoredServer> unlicensedServers) CalculateLicensedAndUnlicensedServers(
            IDictionary<string, IEnumerable<Server>> allServers, int availableLicenseCount)
        {
            // Rank them by the oldest first, as we give licensing preference to longer lived servers over newly
            // registered servers.
            var rankedServers = allServers
                .SelectMany(pair => pair.Value.Select(server => new MonitoredServer(pair.Key, server)))
                .OrderBy(x => x.Server.Added);

            // Decide which servers will and won't be licenced.
            Console.WriteLine($"Available license count: {availableLicenseCount}");

            var licensedServers = new List<MonitoredServer>();
            var unlicensedServers = new List<MonitoredServer>();
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