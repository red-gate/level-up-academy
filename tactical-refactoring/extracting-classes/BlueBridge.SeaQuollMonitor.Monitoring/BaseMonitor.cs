using BlueBridge.SeaQuollMonitor.Domain;

namespace BlueBridge.SeaQuollMonitor.Monitoring
{
    public class BaseMonitor : IBaseMonitor
    {
        public string Name { get; }
        public IMonitoredServersRepository MonitoredServersRepository { get; }

        public BaseMonitor(ISettingsRepository settingsRepository, IMonitoredServersRepository monitoredServersRepository)
        {
            Name = settingsRepository.GetValue("name");
            MonitoredServersRepository = monitoredServersRepository;
        }
    }
}
