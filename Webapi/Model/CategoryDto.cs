namespace Webapi.Model
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<TodoItemDto>? Todos { get; set; }
    }
}
