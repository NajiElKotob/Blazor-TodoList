// Using directives
using System.ComponentModel;
using System.Windows.Input;
using TodoListMauiApp.Services;

namespace TodoListMauiApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged, IDisposable
    {
        // Private fields
        private bool _isConnected;
        private readonly ConnectivityService connectivityService;

        // Public delegates and events
        public event PropertyChangedEventHandler PropertyChanged;

        // Public properties
        public ICommand NavigateToTodoCommand { get; private set; }

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

        public string ConnectionIcon => IsConnected ? "🔗" : "❌";

        // Constructor
        public MainPageViewModel()
        {
            NavigateToTodoCommand = new Command(NavigateToTodo);

            connectivityService = new ConnectivityService();
            connectivityService.ConnectivityChanged += OnConnectivityChanged;

            IsConnected = connectivityService.IsConnected;
        }

        // Public methods
        public void Dispose()
        {
            connectivityService.Dispose();
        }

        // Private methods
        private async void NavigateToTodo()
        {
            // Navigate to the Todo page
            await Shell.Current.GoToAsync($"///{nameof(TodoPage)}");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Event handlers
        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess == NetworkAccess.Internet;
        }
    }
}
