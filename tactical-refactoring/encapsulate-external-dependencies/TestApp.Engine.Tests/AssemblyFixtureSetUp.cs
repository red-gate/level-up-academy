using System.IO;
using NUnit.Framework;

namespace TestApp.Engine.Tests
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
