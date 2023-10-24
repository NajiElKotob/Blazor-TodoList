using System.Windows.Input;

namespace TodoListMauiApp.ViewModels;

public class MainPageViewModel
{
    public ICommand NavigateToTodoCommand { get; private set; }

    public MainPageViewModel()
    {
        NavigateToTodoCommand = new Command(NavigateToTodo);
    }

    private async void NavigateToTodo()
    {
        // Navigate to the Todo page
      await  Shell.Current.GoToAsync($"///{nameof(TodoPage)}");
    }
}
