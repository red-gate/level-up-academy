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
        }
    }
}