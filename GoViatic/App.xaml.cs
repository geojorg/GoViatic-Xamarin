using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
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
            var firstime = Settings.FirstRun;
            
            if (firstime == true)
            {
                Shell.Current.GoToAsync("welcomepage");
            }
            else
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                if (Settings.IsRemembered && token?.Expiration > DateTime.Now)
                {
                    Shell.Current.GoToAsync("//trippage");
                }
                else
                {
                    Shell.Current.GoToAsync("//loginpage");
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
