using System;
using System.IO;
using Serilog;
using Serilog.Core;

namespace ToDoApp.Engine
{
    public static class Log
    {
        private static IToDoLogger? _logger;

        public static IToDoLogger Logger => _logger ?? throw new Exception("Logger has not been initialized yet");

        public static void Initialize(TextWriter textWriter)
        {
            _logger = new SerilogLogger(new LoggerConfiguration().WriteTo.TextWriter(textWriter).CreateLogger());
        }
    }

    public class SerilogLogger : IToDoLogger
    {
        private readonly Logger _logger;
        public SerilogLogger(Logger logger) => _logger = logger;
        public void Information(string s) => _logger.Information(s);
    }

    public interface IToDoLogger
    {
        void Information(string s);
    }
}
