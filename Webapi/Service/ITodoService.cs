using Webapi.Model;

namespace Webapi.Service
{
    public interface ITodoService
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(int id);
        TodoItem Add(TodoItem item);
        TodoItem? Update(int id, TodoItem updated);
        bool Delete(int id);
    }
}
