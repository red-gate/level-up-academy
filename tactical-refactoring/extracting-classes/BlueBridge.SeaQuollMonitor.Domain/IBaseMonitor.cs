namespace BlueBridge.SeaQuollMonitor.Domain
{
    public interface IBaseMonitor
    {
        string Name { get; }

        IMonitoredServersRepository MonitoredServersRepository { get; }
    }
}
