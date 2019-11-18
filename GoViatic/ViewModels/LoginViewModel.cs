using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _password;
        private string _email;
        private string _emptyString;
        private bool _isRemembered;

        public LoginViewModel()
        {
            EmptyString = "Transparent";
            IsRemembered = true;
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

        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password))
                    {
                        EmptyString = "Red";
                    }
                    else
                    {
                        EmptyString = "Transparent";
                        Email = string.Empty;
                        Password = string.Empty;
                    }
                });
            }
        }
        public ICommand RegisterCommand
        {
            get 
            { 
                return new Command(() => 
                { Shell.Current.GoToAsync("//Register"); });
            }
        }
        public ICommand RecoverPswCommand
        {
            get
            {
                return new Command(() =>
                { Application.Current.MainPage.DisplayAlert("Password Recovery", "An Email has been sent to recover your password", "Ok"); });
            }
        }
    }
}
