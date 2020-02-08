using GoViatic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsPage : ContentPage
    {
        public TermsPage()
        {
            InitializeComponent();
            BindingContext = new TermsViewModel();
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}