using Microsoft.EntityFrameworkCore;
using Webapi.Model;

namespace Webapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(c => c.TodoItems)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);
        }

    }
}
