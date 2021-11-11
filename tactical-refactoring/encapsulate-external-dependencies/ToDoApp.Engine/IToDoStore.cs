using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Engine
{
    public interface IToDoStore
    {
        public Task<IReadOnlyCollection<ToDoItem>> GetToDoItemsAsync();
        public Task UpdateToDoItemsAsync(IReadOnlyCollection<ToDoItem> items);
    }
}
