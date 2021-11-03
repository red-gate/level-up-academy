using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace TestApp.Engine.Tests
{
    [TestFixture]
    public sealed class ToDoListTests
    {
        [Test]
        public async Task GetEmptyList()
        {
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(Array.Empty<ToDoItem>());
            var list = new ToDoList(store);

            var actual = await list.GetItemsAsync();
            Assert.That(actual, Is.Empty);
        }

        [Test]
        public async Task GetListWithOneItem()
        {
            var expected = new List<ToDoItem> { new ToDoItem(false, "An item to do") };
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(expected);
            var list = new ToDoList(store);

            var actual = await list.GetItemsAsync();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetListWithThreeItems()
        {
            var expected = new List<ToDoItem>
            {
                new ToDoItem(false, "An item to do"),
                new ToDoItem(true, "An item that is done"),
                new ToDoItem(true, "An item done long ago")
            };
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(expected);
            var list = new ToDoList(store);

            var actual = await list.GetItemsAsync();
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public async Task AddItemToEmptyList()
        {
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(Array.Empty<ToDoItem>());
            IReadOnlyCollection<ToDoItem>? actual = null;
            await store.UpdateToDoItemsAsync(Arg.Do<IReadOnlyCollection<ToDoItem>>(newList => actual = newList));
            var list = new ToDoList(store);
            var newItem = new ToDoItem(false, "New item");

            await list.AddItemAsync(newItem);

            await store.Received(1).UpdateToDoItemsAsync(Arg.Any<IReadOnlyCollection<ToDoItem>>());
            Assert.That(actual, Is.EqualTo(new[] { newItem }));
        }

        [Test]
        public async Task AddItemToExistingList()
        {
            var item1 = new ToDoItem(false, "An item to do");
            var item2 = new ToDoItem(true, "An item that is done");
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(new[] { item1, item2 });
            IReadOnlyCollection<ToDoItem>? actual = null;
            await store.UpdateToDoItemsAsync(Arg.Do<IReadOnlyCollection<ToDoItem>>(newList => actual = newList));
            var list = new ToDoList(store);
            var newItem = new ToDoItem(false, "New item");

            await list.AddItemAsync(newItem);

            await store.Received(1).UpdateToDoItemsAsync(Arg.Any<IReadOnlyCollection<ToDoItem>>());
            Assert.That(actual, Is.EqualTo(new[] { item1, item2, newItem }));
        }

        [Test]
        public async Task AddItemInMiddleOfExistingList()
        {
            var item1 = new ToDoItem(false, "An item to do");
            var item2 = new ToDoItem(true, "An item that is done");
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(new[] { item1, item2 });
            IReadOnlyCollection<ToDoItem>? actual = null;
            await store.UpdateToDoItemsAsync(Arg.Do<IReadOnlyCollection<ToDoItem>>(newList => actual = newList));
            var list = new ToDoList(store);
            var newItem = new ToDoItem(false, "New item");

            await list.AddItemAsync(newItem, 1);

            await store.Received(1).UpdateToDoItemsAsync(Arg.Any<IReadOnlyCollection<ToDoItem>>());
            Assert.That(actual, Is.EqualTo(new[] { item1, newItem, item2 }));
        }

        [Test]
        public async Task RemoveItemInMiddleOfExistingList()
        {
            var item1 = new ToDoItem(false, "An item to do");
            var item2 = new ToDoItem(true, "An item that is done");
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(new[] { item1, item2 });
            IReadOnlyCollection<ToDoItem>? actual = null;
            await store.UpdateToDoItemsAsync(Arg.Do<IReadOnlyCollection<ToDoItem>>(newList => actual = newList));
            var list = new ToDoList(store);

            await list.RemoveItemAsync(item2);

            await store.Received(1).UpdateToDoItemsAsync(Arg.Any<IReadOnlyCollection<ToDoItem>>());
            Assert.That(actual, Is.EqualTo(new[] { item1 }));
        }

        [Test]
        public async Task CompleteItem()
        {
            var item1 = new ToDoItem(false, "An item to do");
            var item2 = new ToDoItem(true, "An item that is done");
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(new[] { item1, item2 });
            IReadOnlyCollection<ToDoItem>? actual = null;
            await store.UpdateToDoItemsAsync(Arg.Do<IReadOnlyCollection<ToDoItem>>(newList => actual = newList));
            var list = new ToDoList(store);

            await list.CompleteItemAsync(item1);

            await store.Received(1).UpdateToDoItemsAsync(Arg.Any<IReadOnlyCollection<ToDoItem>>());
            Assert.That(actual, Is.EqualTo(new[] { item1 with { Complete = true }, item2 }));
        }

        [Test]
        public async Task UncompleteItem()
        {
            var item1 = new ToDoItem(false, "An item to do");
            var item2 = new ToDoItem(true, "An item that is done");
            var store = Substitute.For<IToDoStore>();
            store.GetToDoItemsAsync().Returns(new[] { item1, item2 });
            IReadOnlyCollection<ToDoItem>? actual = null;
            await store.UpdateToDoItemsAsync(Arg.Do<IReadOnlyCollection<ToDoItem>>(newList => actual = newList));
            var list = new ToDoList(store);

            await list.UncompleteItemAsync(item2);

            await store.Received(1).UpdateToDoItemsAsync(Arg.Any<IReadOnlyCollection<ToDoItem>>());
            Assert.That(actual, Is.EqualTo(new[] { item1, item2 with { Complete = false } }));
        }
    }
}
