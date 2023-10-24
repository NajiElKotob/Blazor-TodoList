using TodoListMauiApp.ViewModels;

namespace TodoListMauiApp;

public partial class TodoPage : ContentPage
{
	public TodoPage()
	{
		InitializeComponent();
        BindingContext = new TodoViewModel();
    }
}