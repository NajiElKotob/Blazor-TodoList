using System.Text.Json;
using TodoListMauiApp.Models;

namespace TodoListMauiApp.Services;

/// <summary>
/// Provides methods to interact with the Todo API.
/// </summary>
public class TodoService
{
    // HTTP client to communicate with the Todo API.
    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="TodoService"/> class.
    /// </summary>
    public TodoService()
    {
        client = new HttpClient
        {
            // Base URL of the Todo API.
            BaseAddress = new Uri("https://localhost:5099/")
        };
    }

    /// <summary>
    /// Loads the list of Todo items from the API.
    /// </summary>
    /// <returns>A list of Todo items or null if an error occurs.</returns>
    public async Task<List<TodoItem>> LoadTodosAsync()
    {
        var response = await client.GetAsync("todos");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TodoItem>>(json);
        }

        // Returns null if there's an issue with the API response.
        // Consider throwing an exception for better error handling.
        return null;
    }

    // ToDo: Additional methods for adding, removing, updating, etc., to be implemented.
}
