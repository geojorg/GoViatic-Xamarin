using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.ViewModels;
using GoViatic.Views;
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
        //TODO: FIX THE LOGOUT AND LOGIN BUG
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var firstime = Preferences.Get("firstRun", string.Empty);
            if (firstime == "Yes")
            {
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
