namespace BlueBridge.SeaQuollMonitor.Domain
{
    public interface ISettingsRepository
    {
        string BaseDirectory { get; }

        string GetValue(string name);
    }
}
