using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _entryEmpty;
        private string _name;
        private string _company;
        private string _email;
        private string _password;
        private string _confirmpassoword;
        
        public string EntryEmpty
        {
            get { return _entryEmpty; }
            set { SetProperty(ref _entryEmpty, value); }
        }
        public string Name  
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
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
        public string ConfirmPassword
        {
            get { return _confirmpassoword; }
            set { SetProperty(ref _confirmpassoword, value); }
        }

        public RegisterViewModel()
        {
            EntryEmpty = "Transparent";
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if (string.IsNullOrEmpty(_name)||string.IsNullOrEmpty(_email)||string.IsNullOrEmpty(_password)||string.IsNullOrEmpty(_confirmpassoword)||Password != ConfirmPassword)
                    {
                        EntryEmpty = "Red";
                    }
                    else
                    {
                        EntryEmpty = "Transparent";
                        await Application.Current.MainPage.DisplayAlert("Registration", "Thanks for Registering", "OK");
                        await Shell.Current.GoToAsync("//Login");
                        Name = string.Empty;
                        Company = string.Empty;
                        Email = string.Empty;
                        Password = string.Empty;
                        ConfirmPassword = string.Empty;
                    }
                });
            }
        }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                { 
                    Shell.Current.GoToAsync("//Login");
                    Name = string.Empty;
                    Company = string.Empty;
                    Email = string.Empty;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;
                });
            }
        }
    }
}
