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
        private string _lastName;

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
        public string ConfirmPassword
        {
            get { return _confirmpassoword; }
            set { SetProperty(ref _confirmpassoword, value); }
        }

        public RegisterViewModel()
        {
            EntryEmpty = "Transparent";
        }

        //TODO: CREATE THE REGISTRATION IN THE SYSTEM
        public ICommand RegisterCommand => new Command(RegisterAsync);
        private async void RegisterAsync()
        {
            if (string.IsNullOrEmpty(_name) || string.IsNullOrEmpty(_lastName) || string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_confirmpassoword) || Password != ConfirmPassword)
            {
                EntryEmpty = "Red";
            }
            else
            {
                EntryEmpty = "Transparent";
                await Application.Current.MainPage.DisplayAlert("Registration", "Thanks for Registering", "OK");
                await Shell.Current.Navigation.PopAsync();
                Name = string.Empty;
                LastName = string.Empty;
                Company = string.Empty;
                Email = string.Empty;
                Password = string.Empty;
                ConfirmPassword = string.Empty;
            }
        }

        public ICommand LoginCommand => new Command(Login);
        private void Login()
        {
            Shell.Current.Navigation.PopAsync();
            Name = string.Empty;
            LastName = string.Empty;
            Company = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }
}
