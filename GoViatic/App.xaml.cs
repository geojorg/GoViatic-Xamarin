using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoViatic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            //var firstime = Settings.FirstRun;
            var firstime = true;
            if (firstime == true)
            {
                Routing.RegisterRoute("WelcomePage", typeof(WelcomePage));
                Shell.Current.GoToAsync("WelcomePage");
            }
            else
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
                {
                    Shell.Current.GoToAsync("//TripPage");
                }
                else
                {
                    Shell.Current.GoToAsync("//LoginPage");
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
