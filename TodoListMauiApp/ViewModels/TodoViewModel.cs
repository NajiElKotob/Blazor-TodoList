using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TodoListMauiApp.Models;

namespace TodoListMauiApp.ViewModels;


internal class TodoViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<TodoItem> Todos { get; set; }
    public ICommand AddNewTodoCommand { get; set; }
    public ICommand RemoveTodoCommand { get; set; }

}
