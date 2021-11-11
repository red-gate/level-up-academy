using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToDoApp.Engine
{
    public sealed class JsonToDoStore : IToDoStore
    {
        private sealed record Store(ToDoItem[] Items);
        
        private readonly string _file;

        public JsonToDoStore(string file)
        {
            _file = file;
        }

        public async Task<IReadOnlyCollection<ToDoItem>> GetToDoItemsAsync()
        {
            await using var stream = File.Open(_file, FileMode.Open, FileAccess.Read, FileShare.Read);
            Store? store;
            try
            {
                store = await JsonSerializer.DeserializeAsync<Store>(stream);
            }
            catch (JsonException e)
            {
                throw new InvalidDataException($"To do store {_file} contains invalid data", e);
            }

            if (store == null)
            {
                throw new InvalidDataException($"To do store {_file} deserialized as null");
            }

            return store.Items;
        }

        public async Task UpdateToDoItemsAsync(IReadOnlyCollection<ToDoItem> items)
        {
            await using var stream = File.Open(_file, FileMode.Create, FileAccess.Write, FileShare.None);
            var store = new Store(items.ToArray());
            await JsonSerializer.SerializeAsync(stream, store);
        }
    }
}
