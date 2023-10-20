using TodoListAPI.Models;

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
        app.MapGet("/todos", (TodoListContext context) =>
        {
            return context.Todos.ToList();
        });

        app.MapGet("/todos/{id}", (TodoListContext context, int id) =>
        {
            return context.Todos.Find(id) is Todo todo ? Results.Ok(todo) : Results.NotFound();
        });

        app.MapPut("/todos/{id}", async (TodoListContext context, int id, Todo updatedTodo) =>
        {
            var todo = context.Todos.Find(id);
            if (todo == null) return Results.NotFound();

            todo.Title = updatedTodo.Title;
            todo.IsDone = updatedTodo.IsDone;

            await context.SaveChangesAsync();
            return Results.Ok(todo);
        });

        app.MapDelete("/todos/{id}", async (TodoListContext context, int id) =>
        {
            var todo = context.Todos.Find(id);
            if (todo == null) return Results.NotFound();

            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return Results.Ok();
        });


        app.MapPost("/todos", async (TodoListContext context, Todo todo) =>
        {
            context.Todos.Add(todo);
            await context.SaveChangesAsync();
            return Results.Created($"/todos/{todo.Id}", todo);
        });

    }
}
