using Autofac;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class ManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LicenseService>().As<ILicenseService>().SingleInstance();
            builder.RegisterType<FeaturesClientFactory>().As<IFeaturesClientFactory>().SingleInstance();
            builder.RegisterType<BaseMonitorRegistry>().As<IBaseMonitorRegistry>().SingleInstance();
            builder.RegisterType<LicenseAllocator>().As<ILicenseAllocator>().SingleInstance();
            builder.RegisterType<ServersFetcher>().As<IServersFetcher>().SingleInstance();
            builder.Register<LicenseAllocationAlgorithm>(
                _ => DefaultLicenseAllocationAlgorithm.CalculateLicensedServers);
        }
    }
}
