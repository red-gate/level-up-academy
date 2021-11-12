using System.IO;
using Serilog;
using Serilog.Core;
using ToDoApp.Engine;

namespace ToDoApp.Logging.Serilog
{
    public class SerilogLogger : IToDoLogger
    {
        private readonly Logger _logger;
        public SerilogLogger(Logger logger) => _logger = logger;
        public void Information(string s) => _logger.Information(s);

        public static SerilogLogger Initialize(TextWriter textWriter)
        {
            return new SerilogLogger(new LoggerConfiguration().WriteTo.TextWriter(textWriter).CreateLogger());
        }
    }
}