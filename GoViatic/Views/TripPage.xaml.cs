using GoViatic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripPage : ContentPage
    {
        public TripPage()
        {
            InitializeComponent();
            BindingContext = new TripViewModel();
        }
    }
}