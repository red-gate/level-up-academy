using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlueBridge.SeaQuollMonitor.Management;
using BlueBridge.SeaQuollMonitor.Utilities;

namespace BlueBridge.SeaQuollMonitor.Console
{
    class Program
    {
        static async Task Main()
        {
            // Dependency declarations.
            var builder = new ContainerBuilder();
            builder.RegisterModule<ManagementModule>();
            await using var container = builder.Build();

            // Initialize some base monitors.
            var bmInitializer = new BaseMonitorInitializer(container);
            foreach (var subPath in new[] {"BaseMonitorA", "BaseMonitorB", "BaseMonitorC"})
            {
                bmInitializer.InitializeBaseMonitor(
                    Path.Combine(Constants.ApplicationDataFolder, subPath, "settings.json"));
            }

            // Now run a loop, allowing people to force a licence reallocation.
            System.Console.WriteLine("Press R to refresh the licences or any other key to exit");
            var licenseAllocator = container.Resolve<ILicenseAllocator>();
            var baseMonitorRegistry = container.Resolve<IBaseMonitorRegistry>();
            var repeat = true;
            do
            {
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.R:
                        await licenseAllocator.Refresh();
                        await ShowMonitoredEstate(baseMonitorRegistry);
                        break;

                    default:
                        repeat = false;
                        break;
                }
            } while (repeat);
        }

        private static async Task ShowMonitoredEstate(IBaseMonitorRegistry registry)
        {
            var info = await registry.ExecuteOnAllBaseMonitorsAsync(async baseMonitor =>
            {
                var builder = new StringBuilder();
                builder.AppendLine($"Base Monitor: {baseMonitor.Name}");
                foreach (var server in await baseMonitor.MonitoredServersRepository.GetAllServers())
                {
                    builder.AppendLine($"    {server.Name}\tAdded {server.Added:u}, {(server.IsLicensed ? "Licensed" : "Unlicensed")}, {(server.IsSuspended ? "Suspended" : "Active")}");
                }

                return builder.ToString();
            });

            foreach (var item in info)
            {
                System.Console.Write(item.Value);
            }
        }
    }
}
