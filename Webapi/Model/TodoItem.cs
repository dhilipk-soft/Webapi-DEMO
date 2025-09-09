namespace Webapi.Model
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        // Foreign key
        public int CategoryId { get; set; }

        // Navigation property
        public virtual Category Category { get; set; } = null!;
    }
}
