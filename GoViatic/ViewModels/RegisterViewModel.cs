using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _entryEmpty;
        private string _firstName;
        private string _company;
        private string _email;
        private string _password;
        private string _passwordConfirm;
        private string _lastName;
        private string _alertDialog;
        private bool _isEnable;
        private readonly IApiService _apiService;
        private bool _isRunning;

        public string EntryEmpty
        {
            get { return _entryEmpty; }
            set { SetProperty(ref _entryEmpty, value); }
        }
        public string FirstName  
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        public string Company
        {
            get { return _company; }
            set { SetProperty(ref _company, value); }
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
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set { SetProperty(ref _passwordConfirm, value); }
        }
        public string AlertDialog
        {
            get { return _alertDialog; }
            set { SetProperty(ref _alertDialog, value); }
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

        public RegisterViewModel()
        {
            EntryEmpty = "Transparent";
            IsEnable = true;
            IApiService apiService = new ApiService();
            _apiService = apiService;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                AlertDialog = "You must enter your First Name";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                AlertDialog = "You must enter your Last Name";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(Company))
            {
                AlertDialog = "You must enter your Company";
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

            if (string.IsNullOrEmpty(PasswordConfirm))
            {
                AlertDialog = "You must enter a valid Password Confirmation";
                EntryEmpty = "Red";
                return false;
            }

            if (!Password.Equals(PasswordConfirm))
            {
                AlertDialog = "Confirmation does not match your Password";
                EntryEmpty = "Red";
                return false;
            }
            return true;
        }

        public ICommand RegisterCommand => new Command(RegisterAsync);
        private async void RegisterAsync()
        {
            IsEnable = false;
            IsRunning = true;
            var isValid = ValidateData();
            if (!isValid)
            {
                IsEnable = true;
                IsRunning = false;
                return;
            }

            EntryEmpty = "Transparent";
            var request = new UserRequest
            {
                FirstName = FirstName,
                LastName = LastName,
                Company = Company,
                Email = Email,
                Password = Password,
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.RegisterUserAsync(
                url,
                "api",
                "/Account",
                request);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message,"Accept");
                return;
            }
            IsEnable = true;
            IsRunning = false;
            await App.Current.MainPage.DisplayAlert("Registration Successful", "Confirm your Email to Log In", "Accept");
            await Shell.Current.Navigation.PopModalAsync();
            FirstName = string.Empty;
            LastName = string.Empty;
            Company = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            PasswordConfirm = string.Empty;
        }

        public ICommand TermsCommand => new Command(Terms);
        private void Terms()
        {
            var page = "terms";
            Routing.RegisterRoute("TermsPage", typeof(TermsPage));
            Shell.Current.GoToAsync($"TermsPage?type={page}");
        }

        public ICommand PrivacyCommand => new Command(Privacy);
        private void Privacy()
        {
            var page = "privacy";
            Routing.RegisterRoute("TermsPage", typeof(TermsPage));
            Shell.Current.GoToAsync($"TermsPage?type={page}");
        }

    }
}
