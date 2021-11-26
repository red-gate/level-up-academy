using Autofac;

namespace BlueBridge.SeaQuollMonitor.Management
{
    public class ManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LicenseService>().As<ILicenseService>().SingleInstance();
            builder.RegisterType<BaseMonitorRegistry>().As<IBaseMonitorRegistry>().SingleInstance();
            builder.RegisterType<ServerRetriever>().As<IServerRetriever>().SingleInstance();
            builder.RegisterType<LicenseAlgorithm>().As<ILicenseAlgorithm>().SingleInstance();
            builder.RegisterType<ServerLicenseUpdater>().As<IServerLicenseUpdater>().SingleInstance();
            builder.RegisterType<LicenseAllocator>().As<ILicenseAllocator>().SingleInstance();
        }
    }
}
