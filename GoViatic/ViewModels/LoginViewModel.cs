using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Views;
using Newtonsoft.Json;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool _isEnable;
        private bool _isRunning;
        private bool _isRemembered;
        private string _entryEmpty;
        private string _alertDialog;
        private string _password;
        public static string _email;
        public static string token;
        private readonly IApiService _apiService;

        public LoginViewModel()
        {
            IsEnable = true;
            IsRemember = true;
            EntryEmpty = "Transparent";
            IApiService apiService = new ApiService();
            _apiService = apiService;
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }
        public bool IsRemember
        {
            get { return _isRemembered; }
            set { SetProperty(ref _isRemembered, value); }
        }
        public string EntryEmpty
        {
            get { return _entryEmpty; }
            set { SetProperty(ref _entryEmpty, value); }
        }
        public string AlertDialog
        {
            get { return _alertDialog; }
            set { SetProperty(ref _alertDialog, value); }
        }
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public ICommand LoginCommand => new Command(Login);
        public async void Login()
        {
            var isValid = ValidateData();
            if (!isValid)
            {
                IsEnable = true;
                IsRunning = false;
                return;
            }

            EntryEmpty = "Transparent";
            IsEnable = false;
            IsRunning = true;
            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnection();
            if (!connection)
            {
                IsRunning = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Check Internet Connection", "OK");
                IsRunning = false;
            }
            else
            {
                var response = await _apiService.GetTokenAsync(
                    url,
                    "Account",
                    "/CreateToken",
                    request);

                if (!response.IsSuccess)
                {
                    AlertDialog = "Please Check your Password";
                    EntryEmpty = "Red";
                    Password = string.Empty;
                    IsRunning = false;
                    IsEnable = true;
                    return;
                }
                var tokenData = response.Result;

                var response2 = await _apiService.GetTravelerByEmail(
                    url,
                    "api",
                    "/Travelers/GetTravelerByEmail",
                    "bearer",
                    tokenData.Token,
                    Email);
                var traveler = response2.Result;

                Settings.Traveler = JsonConvert.SerializeObject(traveler);
                Settings.Token = JsonConvert.SerializeObject(tokenData);
                Settings.IsRemembered = IsRemember;

                await Shell.Current.GoToAsync("//TripPage");
                EntryEmpty = "Transparent";
                AlertDialog = string.Empty;
                IsEnable = true;
                IsRunning = false;
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(Email))
            {
                AlertDialog = "You must enter your Email";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                AlertDialog = "You must enter your Password";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(Email) || !RegexHelper.IsValidEmail(Email))
            {
                AlertDialog = "You must enter a valid Email";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(Password) || Password.Length < 6)
            {
                AlertDialog = "You must enter a valid Password";
                EntryEmpty = "Red";
                return false;
            }
            return true;
        }

        public ICommand RegisterCommand => new Command(Register);
        private void Register()
        {
            Routing.RegisterRoute("RegisterPage", typeof(RegisterPage));
            Shell.Current.GoToAsync("RegisterPage");
        }

        public ICommand RecoverPswCommand => new Command(Recovery);
        private void Recovery()
        {
            Shell.Current.Navigation.PushAsync(new RecoverPswPage());
        }
    }
}
