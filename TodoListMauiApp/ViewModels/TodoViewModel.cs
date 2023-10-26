using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TodoListMauiApp.Models;
using TodoListMauiApp.Services;

namespace TodoListMauiApp.ViewModels;

internal class TodoViewModel : INotifyPropertyChanged
{
    private bool isLoading = true;

    private string newTodo;

    private readonly TodoService todoService;

    public ICommand AddTodoCommand { get; set; }
    public ObservableCollection<TodoItem> Todos { get; set; }
    public ICommand AddNewTodoCommand { get; set; }
    public ICommand RemoveTodoCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public TodoViewModel()
    {
        todoService = new TodoService();


        Todos = new ObservableCollection<TodoItem>();

        AddTodoCommand = new Command(async () => await AddNewTodo());
        RemoveTodoCommand = new Command<TodoItem>(RemoveTodo);

        LoadTodoList();
    }

    private async void LoadTodoList()
    {

        var loadedTodos = await todoService.LoadTodosAsync();

        await Task.Delay(2000); // Waits for 5 seconds without blocking the main thread

        IsLoading = false;

        if (loadedTodos != null)
        {
            Todos.Clear();
            foreach (var todo in loadedTodos)
            {
                Todos.Add(todo);
            }
        }
    }

    private async Task AddNewTodo()
    {
        // Implementation here...
        NewTodo = string.Empty;
    }

    private void RemoveTodo(TodoItem todo)
    {
        if (todo != null)
        {
            Todos.Remove(todo);
        }
    }

    public string NewTodo
    {
        get => newTodo;
        set
        {
            if (newTodo != value)
            {
                newTodo = value;
                OnPropertyChanged(nameof(NewTodo));
            }
        }
    }

   
    public bool IsLoading
    {
        get => isLoading;
        set
        {
            isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}

