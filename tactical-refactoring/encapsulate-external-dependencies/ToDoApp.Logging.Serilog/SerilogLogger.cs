using System.IO;
using Serilog;
//using Serilog.Core;
//using ILogger = ToDoApp.Engine.ILogger;

namespace ToDoApp.Logging.Serilog
{
    public class SerilogLogger : ToDoApp.Engine.ILogger
    {
        private readonly global::Serilog.ILogger _logger;
        public SerilogLogger(TextWriter textWriter) => _logger = new LoggerConfiguration().WriteTo.TextWriter(textWriter).CreateLogger();
        public void Information(string message)
        {
            _logger.Information(message);
        }
    }
}