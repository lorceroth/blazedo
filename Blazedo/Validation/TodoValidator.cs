using Blazedo.Models;
using FluentValidation;

namespace Blazedo.Validation
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        public TodoValidator()
        {
            RuleFor(t => t.Description)
                .NotEmpty()
                .WithMessage("Description cannot be empty.");
        }
    }
}
