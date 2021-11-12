using System.IO;
using Serilog;
using Serilog.Core;
using ILogger = ToDoApp.Engine.ILogger;

namespace ToDoApp.Logging.Serilog
{
    public class SerilogLogger : ILogger
    {
        private readonly Logger _logger;
        public SerilogLogger(TextWriter textWriter) => _logger = new LoggerConfiguration().WriteTo.TextWriter(textWriter).CreateLogger();
        public void Information(string s) => _logger.Information(s);
    }
}