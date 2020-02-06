using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Essentials;
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

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var firstime = Preferences.Get("firstRun", string.Empty);
            if (firstime == "Yes")
            {
                MainPage = new AppShell();
                
                if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
                {
                    Shell.Current.GoToAsync("//TripPage");
                }
                else
                {
                    Shell.Current.GoToAsync("//LoginPage");
                }
            }
            else
            {
                MainPage = new AppShell();
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
