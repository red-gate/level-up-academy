using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using TestApp.Engine;

namespace TestApp.Tests
{
    [TestFixture]
    public sealed class GoldenMasterTests
    {
        private string _tempPath = null!;
        private string _storePath = null!;
        private JsonToDoStore _store = null!;

        [OneTimeSetUp]
        public void CreateTempDirectory()
        {
            _tempPath = Path.Combine(Path.GetTempPath(), nameof(GoldenMasterTests) + "_" + Path.GetRandomFileName());
            Directory.CreateDirectory(_tempPath);
        }

        [OneTimeTearDown]
        public void DeleteTempDirectory()
        {
            try
            {
                Directory.Delete(_tempPath, true);
            }
            catch
            {
                Console.WriteLine($"Failed to clean up temp directory {_tempPath}");
            }
        }

        [SetUp]
        public void CreateStore()
        {
            _storePath = Path.Combine(_tempPath, TestContext.CurrentContext.Test.Name + ".json");
            _store = new JsonToDoStore(_storePath);
        }

        [Test]
        public async Task ListItems()
        {
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(false, "Still to do"),
                new ToDoItem(true, "Already done")
            });
            var result = Run("list", "--store", _storePath);
            Assert.That(result.ExitCode, Is.Zero, $"Exit code should be zero.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            AssertEqualIgnoringNewlineDifferences(result.StdOut, @"☐ Still to do
☑ Already done
");
            Assert.That(result.StdErr, Is.Empty);
        }

        [Test]
        public async Task AddItem()
        {
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(false, "Still to do"),
                new ToDoItem(true, "Already done")
            });
            const string newItem = "New item";
            var result = Run("add", newItem, "--store", _storePath);
            Assert.That(result.ExitCode, Is.Zero, $"Exit code should be zero.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            Assert.That(result.StdOut, Is.Empty);
            Assert.That(result.StdErr, Contains.Substring("Added item at end: " + newItem));
        }

        private static Result Run(params string[] args)
        {
            var stdout = new StringWriter();
            var stderr = new StringWriter();
            var exitCode = Program.Run(args, stdout, stderr);
            return new Result(exitCode, stdout.ToString(), stderr.ToString());
        }

        private sealed record Result(int ExitCode, string StdOut, string StdErr);

        private static void AssertEqualIgnoringNewlineDifferences(string actual, string expected)
        {
            var actualNormalized = actual.Replace("\r\n", "\n").Replace("\r", "\n");
            var expectedNormalized = expected.Replace("\r\n", "\n").Replace("\r", "\n");
            Assert.That(actualNormalized, Is.EqualTo(expectedNormalized));
        }
    }
}
