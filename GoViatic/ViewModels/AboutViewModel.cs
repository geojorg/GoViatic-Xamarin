using GoViatic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            AppVersion = $"Version: {VersionTracking.CurrentVersion}";
            BuildNumber = $"Build: {VersionTracking.CurrentBuild}";
        }

        public string AppVersion { get; set; }
        public string BuildNumber { get; set; }

        public ICommand GitCommand => new Command(Git);
        private void Git()
        {
            var analyticsService = DependencyService.Get<IFirebaseAnalytics>();
            analyticsService.LogEvent("aboutbutton");
            //TODO: CHANGE THIS URL
            Launcher.OpenAsync(new Uri("https://play.google.com/store/apps/dev?id=8991899642415667467"));
        }
        public ICommand WebPageCommand => new Command(WebPage);
        private void WebPage()
        {
            var analyticsService = DependencyService.Get<IFirebaseAnalytics>();
            analyticsService.LogEvent("webpage");
            Launcher.OpenAsync(new Uri("http://geojorgx.com"));
        }
    }
}