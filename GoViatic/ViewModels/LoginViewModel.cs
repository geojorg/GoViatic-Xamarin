using GoViatic.Common.Models;
using GoViatic.Common.Services;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _password;
        private string _email;
        private string _emptyString;
        private bool _isRemembered;
        private string _message;
        private bool _isRunning;
        private readonly IApiService _apiService;

        public LoginViewModel()
        {
            EmptyString = "Transparent";
            IsRemembered = true;
            IApiService apiService = new ApiService();
            _apiService = apiService;
            Email = "jgm@gmail.com";
            Password = "123456";
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

        public bool IsRemembered 
        { 
            get { return _isRemembered; }
            set { SetProperty(ref _isRemembered, value); }
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
                var response = await _apiService.GetTokenAsync(url, "Account", "/CreateToken", request);
                if (!response.IsSuccess)
                {
                    Message = "Please Check your Password";
                    EmptyString = "Red";
                    Password = string.Empty;
                    IsRunning = false;
                    return;
                }
                var data = (TokenResponse)response.Result;
                await SecureStorage.SetAsync("PrivateToken", data.Token);
                var token = data.Token;
                await Shell.Current.GoToAsync($"//TripPage?token={token}&email={Email}");
                EmptyString = "Transparent";
                Message = string.Empty;
                IsRunning = false;
            }
        }
        
        public ICommand RegisterCommand => new Command(Register);
        private void Register()
        {
            Shell.Current.GoToAsync("//Register");
        }

        public ICommand RecoverPswCommand => new Command(Recovery);
        private void Recovery()
        {
            Application.Current.MainPage.DisplayAlert(
                "Password Recovery", 
                "An Email has been sent to recover your password", 
                "Ok"); 
        }
    }
}
