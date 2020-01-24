using GoViatic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditViaticPage : ContentPage
    {
        public EditViaticPage()
        {
            InitializeComponent();
            BindingContext = new EditViaticViewModel();
        }
    }
}