using Serilog;

namespace TestApp.Engine
{
    internal static class Log
    {
        public static ILogger Logger { get; } = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    }
}
