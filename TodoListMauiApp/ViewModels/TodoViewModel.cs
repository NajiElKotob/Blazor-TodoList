using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TodoListMauiApp.Models;
using TodoListMauiApp.Services;

namespace TodoListMauiApp.ViewModels;

internal class TodoViewModel : INotifyPropertyChanged, IDisposable
    {
        private string newTodo;
        private bool _isConnected;

        private readonly TodoService todoService;
        private readonly ConnectivityService connectivityService;

        public ICommand AddTodoCommand { get; set; }
        public ObservableCollection<TodoItem> Todos { get; set; }
        public ICommand AddNewTodoCommand { get; set; }
        public ICommand RemoveTodoCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TodoViewModel()
        {
            todoService = new TodoService();
            connectivityService = new ConnectivityService();

            connectivityService.ConnectivityChanged += OnConnectivityChanged;

            IsConnected = connectivityService.IsConnected;

            Todos = new ObservableCollection<TodoItem>();

            AddTodoCommand = new Command(async () => await AddNewTodo());
            RemoveTodoCommand = new Command<TodoItem>(RemoveTodo);

            LoadTodoList();
        }

        private async void LoadTodoList()
        {
            var loadedTodos = await todoService.LoadTodosAsync();
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

    public bool IsConnected
    {
        get => _isConnected;
        set
        {
            if (_isConnected != value)
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
                OnPropertyChanged(nameof(ConnectionIcon));
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
            if (newTodo != value)
            {
                newTodo = value;
                OnPropertyChanged(nameof(NewTodo));
            }
        }
    }


    protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess == NetworkAccess.Internet;
        }

        public void Dispose()
        {
            connectivityService.Dispose();
        }
    }

