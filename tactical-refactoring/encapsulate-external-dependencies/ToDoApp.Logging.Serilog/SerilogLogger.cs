using System.IO;
using Serilog;

namespace ToDoApp.Logging.Serilog
{
    public class SerilogLogger : ILogger
    {
        private global::Serilog.ILogger _logger;

        public SerilogLogger(TextWriter textWriter)
        {
            _logger = new LoggerConfiguration().WriteTo.TextWriter(textWriter).CreateLogger();
        }

        public void Log(string message)
        {
            _logger.Information(message);
        }
    }
}