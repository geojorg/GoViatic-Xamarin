using GoViatic.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViaticsPage : ContentPage
    {
        public ViaticsPage()
        {
            InitializeComponent();
            BindingContext = new ViaticViewModel();
        }
    }
}