using GoViatic.Common.Helpers;
using GoViatic.Views;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public ICommand LogoutCommand => new Command(Logout);
        private void Logout()
        {
            Settings.IsRemembered = false;
            Shell.Current.GoToAsync("//LoginPage");
        }
        
        public ICommand RateCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public ICommand ShareAppCommand => new Command(ShareApp);
        private async void ShareApp()
        {
            //TODO: CHANGE LINK TO NEW NAME IN APP STORE FROM GOOGLE
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "Compartir App",
                Uri = "https://play.google.com/store/apps/details?id=com.geojorgco.sakuracards"
            });
        }

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}