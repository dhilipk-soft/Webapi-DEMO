using Webapi.Data;
using Webapi.Model;
using Microsoft.EntityFrameworkCore;

namespace Webapi.Service
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TodoItem> GetAll() => _context.TodoItems.ToList();

        public TodoItem? GetById(int id) => _context.TodoItems.Find(id);

        public TodoItem Add(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            return item;
        }

        public TodoItem? Update(int id, TodoItem updated)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo is null) return null;

            todo.Title = updated.Title;
            todo.IsCompleted = updated.IsCompleted;

            _context.SaveChanges();
            return todo;
        }

        public bool Delete(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo is null) return false;

            _context.TodoItems.Remove(todo      );
            _context.SaveChanges();
            return true;
        }
    }
}
