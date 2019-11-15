using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public LoginViewModel()
        {
            IsRemembered = true;
        }

        public bool IsRemembered { get; set; }
        public string Password { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private void Register()
        {
            Shell.Current.GoToAsync("//Register");
        }

        public ICommand RecoverPswCommand
        {
            get
            {
                return new RelayCommand(RecoverPsw);
            }
        }

        private void RecoverPsw()
        {
            Application.Current.MainPage.DisplayAlert("Password Recovery", "An Email has been sent to recover your password", "Ok");
        }
    }
}
