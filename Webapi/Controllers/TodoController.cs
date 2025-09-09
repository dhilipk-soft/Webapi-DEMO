using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webapi.Data;
using Webapi.Model;
using Webapi.Service;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoController> _logger;
        private readonly IMapper _mapper;

        public TodoController(ITodoService todoService, ILogger<TodoController> logger, IMapper mapper)
        {
            _todoService = todoService;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<TodoItemDto>> GetAll()
        {
            var todos = _todoService.GetAll()
                .Select(t => new TodoItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    IsCompleted = t.IsCompleted
                });

            var todoDtos = _mapper.Map<IEnumerable<TodoItemDto>>(todos);

            _logger.LogInformation("Fetching all datas");

            return Ok(todoDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItemDto> GetById(int id)
        {
            var todo = _todoService.GetById(id);
            if (todo is null) return NotFound();

            _logger.LogInformation($"Todo item {todo.Id}");

            return Ok(new TodoItemDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted
            });
        }

        [HttpPost]
        public ActionResult<TodoItemDto> Add(TodoItemDto dto)
        {
            var todo = _mapper.Map<TodoItem>(dto);

            var created = _todoService.Add(todo);

            _logger.LogInformation($"Created {created}");

            var result = new TodoItemDto
            {
                Id = created.Id,
                Title = created.Title,
                IsCompleted = created.IsCompleted
            };

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<TodoItemDto> Update(int id, TodoItemDto dto)
        {
            var updated = new TodoItem
            {
                Id = id,
                Title = dto.Title,
                IsCompleted = dto.IsCompleted
            };

            var todo = _todoService.Update(id, updated);
            
            if (todo is null) {
                _logger.LogError("not found items");
                return NotFound(); }
            _logger.LogInformation($"Updated {todo.Id}");
            return Ok(new TodoItemDto
            {
                Id = todo.Id,
                Title = todo.Title,
                IsCompleted = todo.IsCompleted
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Delete Todo Id {Id}", id);

            bool deleted = _todoService.Delete(id);

            if (!deleted)
            {
                _logger.LogWarning("Todo {Id} not found", id);
                return NotFound(new { Message = $"Todo item with Id {id} not found." });
            }

            _logger.LogInformation("Todo  {Id} deleted", id);
            return NoContent();
        }


    }
}