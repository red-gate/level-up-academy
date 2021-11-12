using System;
using System.IO;
using Serilog;
using ToDoApp.Logging.Serilog;

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
}
