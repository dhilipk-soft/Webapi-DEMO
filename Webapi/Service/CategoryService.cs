using Webapi.Data;
using Webapi.Model;
using Microsoft.EntityFrameworkCore;


namespace Webapi.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories
                           .Include(c => c.TodoItems) // eager load related todos
                           .ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories
                           .Include(c => c.TodoItems)
                           .FirstOrDefault(c => c.Id == id);
        }

        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category? Update(int id, Category updated)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return null;

            category.Name = updated.Name;
            _context.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            var category = _context.Categories
                                   .Include(c => c.TodoItems)
                                   .FirstOrDefault(c => c.Id == id);
            if (category == null) return false;

            // Optional: cascade delete todos
            _context.TodoItems.RemoveRange(category.TodoItems!);

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
    }
