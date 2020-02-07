using GoViatic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePswPage : ContentPage
    {
        public ChangePswPage()
        {
            InitializeComponent();
            Shell.SetTabBarIsVisible(this, false);
            BindingContext = new ChangePswViewModel();
        }
    }
}