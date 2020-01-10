using GoViatic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTripPage : ContentPage
    {
        public CreateTripPage()
        {
            InitializeComponent();
            BindingContext = new CreateTripViewModel();
        }
    }
}