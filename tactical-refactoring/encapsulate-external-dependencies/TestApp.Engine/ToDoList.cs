using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static TestApp.Engine.Log;

namespace TestApp.Engine
{
    public sealed class ToDoList
    {
        private readonly IToDoStore _store;

        public ToDoList(IToDoStore store)
        {
            _store = store;
        }

        public Task<IReadOnlyCollection<ToDoItem>> GetItemsAsync()
        {
            return _store.GetToDoItemsAsync();
        }

        public async Task AddItemAsync(ToDoItem item)
        {
            var items = (await _store.GetToDoItemsAsync()).ToList();
            items.Add(item);
            await _store.UpdateToDoItemsAsync(items);
            Logger.Information($"Added item at end: {item.Item}");
        }

        public async Task AddItemAsync(ToDoItem item, int position)
        {
            var items = (await _store.GetToDoItemsAsync()).ToList();
            items.Insert(position, item);
            await _store.UpdateToDoItemsAsync(items);
            Logger.Information($"Added item at position {position}: {item.Item}");
        }

        public async Task RemoveItemAsync(ToDoItem item)
        {
            var items = (await _store.GetToDoItemsAsync()).ToList();
            items.Remove(item);
            await _store.UpdateToDoItemsAsync(items);
            Logger.Information($"Removed item: {item.Item}");
        }

        public async Task CompleteItemAsync(ToDoItem item)
        {
            var items = (await _store.GetToDoItemsAsync()).ToList();
            items[items.IndexOf(item)] = new ToDoItem(true, item.Item);
            await _store.UpdateToDoItemsAsync(items);
            Logger.Information($"Completed item: {item.Item}");
        }

        public async Task UncompleteItemAsync(ToDoItem item)
        {
            var items = (await _store.GetToDoItemsAsync()).ToList();
            items[items.IndexOf(item)] = new ToDoItem(false, item.Item);
            await _store.UpdateToDoItemsAsync(items);
            Logger.Information($"Uncompleted item: {item.Item}");
        }
    }
}
