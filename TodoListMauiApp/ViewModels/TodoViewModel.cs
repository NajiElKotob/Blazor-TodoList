using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;
using TodoListMauiApp.Models;

namespace TodoListMauiApp.ViewModels;

internal class TodoViewModel : INotifyPropertyChanged
{

    private HttpClient client;
    private string newTodo;

    public ICommand AddTodoCommand { get; set; }

    // Event to notify when a property changes
    public event PropertyChangedEventHandler PropertyChanged;

    // Collection to store and manage Todo items
    public ObservableCollection<TodoItem> Todos { get; set; }

    // Command to handle adding a new Todo item
    public ICommand AddNewTodoCommand { get; set; }

    // Command to handle removing an existing Todo item
    public ICommand RemoveTodoCommand { get; set; }

    private bool _isConnected;

    public TodoViewModel()
    {
        // Initial connectivity status check
        IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;

        // Subscribe to connectivity changes for dynamic updates
        Connectivity.ConnectivityChanged += OnConnectivityChanged;


        client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5099/")
        };

        Todos = new ObservableCollection<TodoItem>();
        AddTodoCommand = new Command(async () => await AddNewTodo());
        RemoveTodoCommand = new Command<TodoItem>(RemoveTodo);

        LoadTodoList();
    }

    private async void LoadTodoList()
    {
        try
        {
            var response = await client.GetAsync("todos");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var loadedTodos = JsonSerializer.Deserialize<List<TodoItem>>(json);

                Todos.Clear();
                foreach (var todo in loadedTodos)
                {
                    Todos.Add(todo);
                }
            }
            else
            {
                // Handle error. Maybe show an alert or a message to the user.
            }
        }
        catch (Exception ex)
        {
            // Handle exception. Maybe log it or show an alert to the user.
        }
    }

    private async Task AddNewTodo()
    {
        // ... Similar to your Blazor code ...

        //Todos.Add(addedTodo);
        NewTodo = string.Empty;
    }

    private void RemoveTodo(TodoItem todo)
    {
        if (todo != null)
        {
            Todos.Remove(todo);
        }
    }

    // Property to track internet connectivity status
    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            if (_isConnected != value)
            {
                _isConnected = value;

                // Notify UI of connectivity status change
                OnPropertyChanged(nameof(IsConnected));
                OnPropertyChanged(nameof(ConnectionIcon)); // Notify about the icon change
            }
        }
    }

    public string ConnectionIcon
    {
        get => IsConnected ? "🔗" : "❌";
    }


    public string NewTodo
    {
        get => newTodo;
        set
        {
            newTodo = value;
            OnPropertyChanged(nameof(NewTodo));
        }
    }


    // Helper method to raise the PropertyChanged event
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Event handler to update IsConnected when connectivity changes
    private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        IsConnected = e.NetworkAccess == NetworkAccess.Internet;
    }

    // Cleanup method to unsubscribe from events, avoiding memory leaks
    public void Dispose()
    {
        Connectivity.ConnectivityChanged -= OnConnectivityChanged;
    }
}
