using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ToDoApp.Engine;

namespace ToDoApp.Tests
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
            _storePath = Path.Combine(_tempPath, TestContext.CurrentContext.Test.Name.Replace('"', '\'') + ".json");
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
            var newList = await _store.GetToDoItemsAsync();
            Assert.That(newList.Select(x => x.Item), Contains.Item(newItem));
        }

        [Test]
        public async Task AddItemAfter()
        {
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(false, "Still to do"),
                new ToDoItem(true, "Already done")
            });
            const string newItem = "New item";
            var result = Run("add", newItem, "--after", "Still to do", "--store", _storePath);
            Assert.That(result.ExitCode, Is.Zero, $"Exit code should be zero.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            Assert.That(result.StdOut, Is.Empty);
            Assert.That(result.StdErr, Contains.Substring("Added item at position 1: " + newItem));
            var newList = await _store.GetToDoItemsAsync();
            Assert.That(newList.Select(x => x.Item), Contains.Item(newItem));
        }

        [Test]
        public async Task AddItemBefore()
        {
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(false, "Still to do"),
                new ToDoItem(true, "Already done")
            });
            const string newItem = "New item";
            var result = Run("add", newItem, "--before", "Already done", "--store", _storePath);
            Assert.That(result.ExitCode, Is.Zero, $"Exit code should be zero.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            Assert.That(result.StdOut, Is.Empty);
            Assert.That(result.StdErr, Contains.Substring("Added item at position 1: " + newItem));
            var newList = await _store.GetToDoItemsAsync();
            Assert.That(newList.Select(x => x.Item), Contains.Item(newItem));
        }

        [Test]
        public async Task CompleteItem()
        {
            const string item = "Still to do";
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(false, item)
            });
            var result = Run("complete", item, "--store", _storePath);
            Assert.That(result.ExitCode, Is.Zero, $"Exit code should be zero.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            Assert.That(result.StdOut, Is.Empty);
            Assert.That(result.StdErr, Contains.Substring("Completed item: " + item));
            var newList = await _store.GetToDoItemsAsync();
            Assert.That(newList.Single().Complete, Is.True);
        }

        [Test]
        public async Task UncompleteItem()
        {
            const string item = "Already done";
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(true, item)
            });
            var result = Run("uncomplete", item, "--store", _storePath);
            Assert.That(result.ExitCode, Is.Zero, $"Exit code should be zero.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            Assert.That(result.StdOut, Is.Empty);
            Assert.That(result.StdErr, Contains.Substring("Uncompleted item: " + item));
            var newList = await _store.GetToDoItemsAsync();
            Assert.That(newList.Single().Complete, Is.False);
        }

        [Test]
        public async Task RemoveItem()
        {
            const string itemToRemove = "Already done";
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(false, "Still to do"),
                new ToDoItem(true, itemToRemove)
            });
            var result = Run("remove", itemToRemove, "--store", _storePath);
            Assert.That(result.ExitCode, Is.Zero, $"Exit code should be zero.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            Assert.That(result.StdOut, Is.Empty);
            Assert.That(result.StdErr, Contains.Substring("Removed item: " + itemToRemove));
            var newList = await _store.GetToDoItemsAsync();
            Assert.That(newList.Select(x => x.Item), Does.Not.Contain(itemToRemove));
        }

        [TestCase("complete")]
        [TestCase("uncomplete")]
        [TestCase("add")]
        [TestCase("remove")]
        public async Task MissingItem_Uncomplete(string command)
        {
            await _store.UpdateToDoItemsAsync(new[]
            {
                new ToDoItem(true, "Already done")
            });
            const string missingItem = "does not exist";
            var args = command == "add"
                ? new[] { command, "foo", "--after", missingItem, "--store", _storePath }
                : new[] { command, missingItem, "--store", _storePath };
            var result = Run(args);
            Assert.That(result.ExitCode, Is.EqualTo(1), $"Exit code should be 1.\nStdout:\n{result.StdOut}\nStderr:\n{result.StdErr}");
            Assert.That(result.StdOut, Is.Empty);
            Assert.That(result.StdErr, Contains.Substring($"No item '{missingItem}' found"));
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
