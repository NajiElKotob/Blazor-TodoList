namespace TodoListMauiApp.Services;


public class ConnectivityService
{
    public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

    public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

    public ConnectivityService()
    {
        Connectivity.ConnectivityChanged += OnConnectivityChanged;
    }

    private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        ConnectivityChanged?.Invoke(this, e);
    }

    public void Dispose()
    {
        Connectivity.ConnectivityChanged -= OnConnectivityChanged;
    }
}
