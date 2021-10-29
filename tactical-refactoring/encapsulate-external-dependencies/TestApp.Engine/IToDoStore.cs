using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp.Engine
{
    public interface IToDoStore
    {
        public Task<IReadOnlyCollection<ToDoItem>> GetToDoItemsAsync();
        public Task UpdateToDoItemsAsync(IReadOnlyCollection<ToDoItem> items);
    }
}
