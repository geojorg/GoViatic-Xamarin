using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class RecoverPswViewModel : BaseViewModel
    {
        private bool _isRunning;
        private string _isVisible;
        private string _imageSource;
        private string _message;
        private readonly IApiService _apiService;

        public RecoverPswViewModel()
        {
            IApiService apiService = new ApiService();
            _apiService = apiService;
            ImageSource = "ic_lock_open";
            IsVisible = "1";
            Message = "Just enter the email address you've use to register in GoViatic";
        }

        public string Email { get; set; }
      
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        public string ImageSource 
        {
            get { return _imageSource; }
            set { SetProperty(ref _imageSource, value); }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }
        public string IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        public ICommand RecoverPswCommand => new Command(RecoverPsw);
        private async void RecoverPsw()
        {
            IsRunning = true;

            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }
            var request = new EmailRequest
            {
                Email = Email
            };
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.RecoverPasswordAsync(
                url,
                "/api",
                "/Account/RecoverPassword",
                request);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message,"Accept");
                return;
            }
            IsVisible = "0";
            ImageSource = "ic_email";
            Message = "Please check your inbox for the password reset link.";
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Email) || !RegexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a valid Email", "Accept");
                return false;
            }
            return true;
        }
    }
}
