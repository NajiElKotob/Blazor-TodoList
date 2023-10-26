namespace TodoListMauiApp.Services;

/// <summary>
/// Provides services related to connectivity status and events.
/// </summary>
public class ConnectivityService
{
    /// <summary>
    /// Gets a value indicating whether the device is connected to the Internet.
    /// </summary>
    public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

    /// <summary>
    /// Event raised when connectivity status changes.
    /// </summary>
    public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConnectivityService"/> class.
    /// Subscribes to the system's connectivity changed event.
    /// </summary>
    public ConnectivityService()
    {
        Connectivity.ConnectivityChanged += OnConnectivityChanged;
    }

    /// <summary>
    /// Handles the system's connectivity changed event and raises the <see cref="ConnectivityChanged"/> event.
    /// </summary>
    private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        ConnectivityChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Unsubscribes from the system's connectivity changed event to avoid memory leaks.
    /// </summary>
    public void Dispose()
    {
        Connectivity.ConnectivityChanged -= OnConnectivityChanged;
    }
}
