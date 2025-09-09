using FluentValidation;
using Webapi.Model;

namespace Webapi.Validator
{
    public class TodoItemDtoValidator : AbstractValidator<TodoItemDto>
    {
        public TodoItemDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.IsCompleted)
                .NotNull();
        }
    }
}
