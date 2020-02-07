using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class ChangePswViewModel : BaseViewModel
    {
        private bool _isRunning;
        private string _alertDialog;
        private string _entryEmpty;
        private readonly IApiService _apiService;

        public ChangePswViewModel()
        {
            IApiService apiService = new ApiService();
            _apiService = apiService;
        }

        public string CurrentPassword { get; set; }
       
        public string NewPassword { get; set; }
        
        public string ConfirmPassword { get; set; }
       
        public string AlertDialog
        {
            get { return _alertDialog; }
            set { SetProperty(ref _alertDialog, value); }
        }
        public string EntryEmpty
        {
            get { return _entryEmpty; }
            set { SetProperty(ref _entryEmpty, value); }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
        }

        public ICommand ChangePswCommand => new Command(ChangePsw);
        private async void ChangePsw()
        {
            IsRunning = true;
            var isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            var traveler = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var request = new ChangePasswordRequest
            {
                Email = traveler.Email,
                NewPassword = NewPassword,
                OldPassword = CurrentPassword
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.ChangePasswordAsync(
                url,
                "/api",
                "/Account/ChangePassword",
                request,
                "bearer",
                token.Token);
            IsRunning = false;

            await App.Current.MainPage.DisplayAlert("Password Change", "The password has been change succefully","Accept");
            await Shell.Current.Navigation.PopAsync();
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                AlertDialog = "You must enter your Current Password";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(NewPassword) || NewPassword?.Length < 6)
            {
                AlertDialog = "You must enter your New Password";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                AlertDialog = "You must enter the Password Confirmation";
                EntryEmpty = "Red";
                return false;
            }
            if (!NewPassword.Equals(ConfirmPassword))
            {
                AlertDialog = "The new password and the confirmation does not match";
                EntryEmpty = "Red";
                return false;
            }
            return true;
        }
    }
}
