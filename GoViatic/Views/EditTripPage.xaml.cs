using GoViatic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTripPage : ContentPage
    {
        public EditTripPage()
        {
            InitializeComponent();
            BindingContext = new EditTripViewModel();
        }
    }
}