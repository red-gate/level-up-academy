using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ToDoApp.Engine.Tests
{
    [TestFixture]
    internal sealed class JsonToDoStoreTests
    {
        private string _tempPath = null!;
        private string _storePath = null!;
        private JsonToDoStore _store = null!;

        [OneTimeSetUp]
        public void CreateTempDirectory()
        {
            _tempPath = Path.Combine(Path.GetTempPath(), nameof(JsonToDoStoreTests) + "_" + Path.GetRandomFileName());
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
        public void ReadingNonExistentFile_ThrowsFileNotFoundException()
        {
            Assert.ThrowsAsync<FileNotFoundException>(async () => await _store.GetToDoItemsAsync());
        }

        [Test]
        public void ReadingEmptyFile_ThrowsInvalidDataException()
        {
            File.WriteAllText(_storePath, "");
            Assert.ThrowsAsync<InvalidDataException>(async () => await _store.GetToDoItemsAsync());
        }
        
        [Test]
        public void ReadingFileWithNullIn_ThrowsInvalidDataException()
        {
            File.WriteAllText(_storePath, "null");
            Assert.ThrowsAsync<InvalidDataException>(async () => await _store.GetToDoItemsAsync());
        }

        [Test]
        public async Task RoundTripEmptyList()
        {
            await _store.UpdateToDoItemsAsync(Array.Empty<ToDoItem>());
            Assert.That(_storePath, Does.Exist);
            var actual = await _store.GetToDoItemsAsync();
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public async Task RoundTripSingleItem()
        {
            var expectedItem = new ToDoItem(false, "Run this test");
            await _store.UpdateToDoItemsAsync(new[] { expectedItem });
            Assert.That(_storePath, Does.Exist);
            var actual = await _store.GetToDoItemsAsync();
            Assert.That(actual, Has.One.Items);
            var actualItem = actual.Single();
            Assert.That(actualItem, Is.EqualTo(expectedItem));
        }

        [Test]
        public async Task RoundTripThreeItems()
        {
            var item1 = new ToDoItem(true, "Run a test");
            var item2 = new ToDoItem(false, "Run this test");
            var item3 = new ToDoItem(false, "Run another test");
            var expected = new[] { item1, item2, item3 };
            await _store.UpdateToDoItemsAsync(expected);
            Assert.That(_storePath, Does.Exist);
            var actual = await _store.GetToDoItemsAsync();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
