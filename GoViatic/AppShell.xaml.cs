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
        public ICommand UserCommand => new Command(User);
        private void User()
        {
            Shell.Current.GoToAsync("userpage");
        }

        public ICommand AboutCommand => new Command(About);
        private void About()
        {
            Shell.Current.GoToAsync("aboutpage");
        }

        public ICommand LogoutCommand => new Command(Logout);
        private void Logout()
        {
            Settings.IsRemembered = false;
            Application.Current.MainPage = new AppShell();
        }
        
        public ICommand RateCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public ICommand ShareAppCommand => new Command(ShareApp);

        private async void ShareApp()
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "Compartir App",
                Uri = "https://play.google.com/store/apps/details?id=com.geojorgco.goviatic"
            });
        }

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
            Routing.RegisterRoute("welcomepage", typeof(WelcomePage));
            Routing.RegisterRoute("aboutpage", typeof(AboutPage));
            Routing.RegisterRoute("userpage", typeof(UserPage));
            Routing.RegisterRoute("registerpage", typeof(RegisterPage));
            Routing.RegisterRoute("recoverpswpage", typeof(RecoverPswPage));
            Routing.RegisterRoute("edittrippage", typeof(EditTripPage));
            Routing.RegisterRoute("viaticpage", typeof(ViaticsPage));

        }
    }
}