using TodoListMauiApp.ViewModels;

namespace TodoListMauiApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
           
        }


    }
}