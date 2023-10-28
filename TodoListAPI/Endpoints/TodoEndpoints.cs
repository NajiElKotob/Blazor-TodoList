using TodoListAPI.Models;
using TodoListAPI.Validators;

namespace TodoListAPI.Endpoints;

/// <summary>
/// Contains extension methods for mapping Todo-related routes.
/// </summary>
public static class TodoEndpoints
{
    /// <summary>
    /// Maps the CRUD operations related to Todos to specific routes.
    /// </summary>
    /// <param name="app">The web application to which the routes are added.</param>
    public static void MapTodoEndpoints(this WebApplication app)
    {

        // Retrieve all todos
        app.MapGet("/hello", () =>
        {
            return "Hello!";
        }).RequireAuthorization();

        // Retrieve all todos
        app.MapGet("/todos", (TodoListContext context) =>
        {
            return context.Todos.ToList();
        });

        // Retrieve a specific todo by its ID
        app.MapGet("/todos/{id}", (TodoListContext context, int id) =>
        {
            return context.Todos.Find(id) is Todo todo ? Results.Ok(todo) : Results.NotFound();
        });

        // Create a new todo
        app.MapPost("/todos", async (TodoListContext context, Todo todo) =>
        {
            // Initialize a new Todo validator and validate the provided todo item.
            // If validation fails, collect all error messages and return them as a validation problem.
            var validator = new TodoValidator();
            var validationResult = validator.Validate(todo);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(err => err.ErrorMessage);
                return Results.ValidationProblem(new Dictionary<string, string[]>
                    {
                        { "Errors", errors.ToArray() }
                    });
            }

            context.Todos.Add(todo);
            await context.SaveChangesAsync();
            return Results.Created($"/todos/{todo.Id}", todo);
        });

        // Update a specific todo by its ID
        app.MapPut("/todos/{id}", async (TodoListContext context, int id, Todo updatedTodo) =>
        {
            var todo = context.Todos.Find(id);
            if (todo == null) return Results.NotFound();

            todo.Title = updatedTodo.Title;
            todo.IsDone = updatedTodo.IsDone;

            await context.SaveChangesAsync();
            return Results.Ok(todo);
        });

        // Delete a specific todo by its ID
        app.MapDelete("/todos/{id}", async (TodoListContext context, int id) =>
        {
            var todo = context.Todos.Find(id);
            if (todo == null) return Results.NotFound();

            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}
