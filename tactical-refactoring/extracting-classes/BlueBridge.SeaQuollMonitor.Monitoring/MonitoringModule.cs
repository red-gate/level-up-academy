using Autofac;
using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Monitoring
{
    public class MonitoringModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BaseMonitor>().As<IBaseMonitor>().InstancePerLifetimeScope();

            builder.RegisterType<MonitoredServersRepository>()
                .As<IMonitoredServersRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterDecorator<ErrorHandlingMonitoredServersRepository, IMonitoredServersRepository>();
        }
    }
}
