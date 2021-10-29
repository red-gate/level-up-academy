using Autofac;
using BlueBridge.SeaQuollMonitor.Domain;
using BlueBridge.SeaQuollMonitor.Management;
using BlueBridge.SeaQuollMonitor.Monitoring;

namespace BlueBridge.SeaQuollMonitor.Console
{
    public class BaseMonitorInitializer
    {
        private readonly IContainer _container;

        public BaseMonitorInitializer(IContainer container)
        {
            _container = container;
        }

        public void InitializeBaseMonitor(string settingsFilePath)
        {
            var lifetimeScope = _container.BeginLifetimeScope(settingsFilePath, builder =>
            {
                builder.RegisterModule<MonitoringModule>();
                builder.Register(_ => new SettingsRepository(settingsFilePath))
                    .As<ISettingsRepository>()
                    .InstancePerLifetimeScope();
            });

            var baseMonitor =  lifetimeScope.Resolve<IBaseMonitor>();

            var registry = _container.Resolve<IBaseMonitorRegistry>();
            registry.AddBaseMonitor(baseMonitor);
        }
    }
}
