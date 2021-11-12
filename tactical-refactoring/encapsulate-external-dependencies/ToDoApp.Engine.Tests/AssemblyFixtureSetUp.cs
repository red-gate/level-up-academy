using System.IO;
using NUnit.Framework;
using ToDoApp.Logging.Serilog;

namespace ToDoApp.Engine.Tests
{
    [SetUpFixture]
    public sealed class AssemblyFixtureSetUp
    {
        [OneTimeSetUp]
        public void InitializeLogging()
        {
            SerilogLogger.Initialize(TextWriter.Null);
        }
    }
}
