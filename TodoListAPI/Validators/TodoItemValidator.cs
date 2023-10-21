using FluentValidation;
using TodoListAPI.Models;

namespace TodoListAPI.Validators;

public class TodoValidator : AbstractValidator<Todo>
{
    public TodoValidator()
    {
        RuleFor(todo => todo.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(3, 50).WithMessage("Title must be between 3 and 50 characters.")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Title can only contain alphanumeric characters and spaces.");

        RuleFor(todo => todo.IsDone)
            .NotNull().WithMessage("IsDone status is required.");
    }
}
