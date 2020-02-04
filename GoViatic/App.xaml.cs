using GoViatic.Common.Helpers;
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

            var firstime = Preferences.Get("firstRun", string.Empty);
            if (firstime == "Yes")
            {
                MainPage = new AppShell();
                //TODO: MAKE THE SETTINGS WORK FROM THE START
                //if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
                //{
                //    Shell.Current.GoToAsync("//TripPage");
                //}
                //else
                //{
                    Shell.Current.GoToAsync("//LoginPage");
                //}
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
