using Xamarin.Essentials;

namespace GoViatic.Common.Helpers
{
    public static class Settings
    {
    
        public static string Traveler 
        {
            get { return Preferences.Get("userData", string.Empty); }          
            set {Preferences.Set("userData", value);}
        }

        public static bool IsRemembered
        {
            get { return Preferences.Get("loginData", false); }
            set { Preferences.Set("loginData", value); }
        }

        public static string Token
        {
            get { return Preferences.Get("oauth_token",string.Empty); }
            set { Preferences.Set("oauth_token", value); }
        }

        public static bool FirstRun
        {
            get { return Preferences.Get("firtsRun", true); }
            set { Preferences.Set("firtsRun", value); }
        }
    }
}
