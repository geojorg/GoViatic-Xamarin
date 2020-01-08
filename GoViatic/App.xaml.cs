using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Globalization;
using System;

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
                Shell.Current.GoToAsync("//Login");
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
