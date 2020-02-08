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
        private string _password;
        public static string _email;
        private string _emptyString;
        private bool _isRemembered;
        private string _message;
        private bool _isRunning;
        public static string token;
        private bool _isEnable;
        private readonly IApiService _apiService;

        public LoginViewModel()
        {
            IsEnable = true;
            EmptyString = "Transparent";
            IsRemember = true;
            IApiService apiService = new ApiService();
            _apiService = apiService;
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
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public bool IsRunning 
        { 
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }
        public string EmptyString 
        { 
            get { return _emptyString; }
            set { SetProperty(ref _emptyString, value); }
        }
        public bool IsRemember 
        { 
            get { return _isRemembered; }
            set { SetProperty(ref _isRemembered, value); }
        }
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }

        public ICommand LoginCommand => new Command(Login);
        public async void Login()
        {
            IsEnable = false;
            EmptyString = "Transparent";
            if (string.IsNullOrEmpty(_email) && string.IsNullOrEmpty(_password))
            {
                Message = "Please Check your Email and Password";
                EmptyString = "Red";
            }
            else if (string.IsNullOrEmpty(_email))
            {
                Message = "Please Check your Email";
                EmptyString = "Red";
            }
            else if (string.IsNullOrEmpty(_password))
            {
                Message = "Please Check your Password";
                EmptyString = "Red";
            }
            else
            {
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
                        Message = "Please Check your Password";
                        EmptyString = "Red";
                        Password = string.Empty;
                        IsRunning = false;
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
                    IsEnable = false;
                    Settings.Traveler = JsonConvert.SerializeObject(traveler);
                    Settings.Token = JsonConvert.SerializeObject(tokenData);
                    Settings.IsRemembered = IsRemember;
                   
                    await Shell.Current.GoToAsync("//TripPage");
                    EmptyString = "Transparent";
                    Message = string.Empty;
                    IsRunning = false;
                }
            }
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
