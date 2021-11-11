using System.IO;
using NUnit.Framework;

namespace ToDoApp.Engine.Tests
{
    [SetUpFixture]
    public sealed class AssemblyFixtureSetUp
    {
        [OneTimeSetUp]
        public void InitializeLogging()
        {
            Log.Initialize(TextWriter.Null);
        }
    }
}
