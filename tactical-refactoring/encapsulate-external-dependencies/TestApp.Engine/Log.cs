using System;
using System.IO;
using Serilog;

namespace TestApp.Engine
{
    public static class Log
    {
        private static ILogger? _logger;

        public static ILogger Logger => _logger ?? throw new Exception("Logger has not been initialized yet");

        public static void Initialize(TextWriter textWriter)
        {
            _logger = new LoggerConfiguration().WriteTo.TextWriter(textWriter).CreateLogger();
        }
    }
}
