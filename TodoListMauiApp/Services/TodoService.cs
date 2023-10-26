using System.Text.Json;
using TodoListMauiApp.Models;

namespace TodoListMauiApp.Services;

public class TodoService
{
    private readonly HttpClient client;

    public TodoService()
    {
        client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5099/")
        };
    }

    public async Task<List<TodoItem>> LoadTodosAsync()
    {
        var response = await client.GetAsync("todos");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TodoItem>>(json);
        }
        return null; // Or throw an exception
    }

    // ToDo: Implement AddTodo, RemoveTodo, ... methods 
}
