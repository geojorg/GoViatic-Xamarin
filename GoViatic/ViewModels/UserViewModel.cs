using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Views;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private MediaFile _file;
        private ImageSource _profileImage;
        private string _alertDialog;
        private TravelerResponse _traveler;
        private string _entryEmpty;
        private bool _isEnable;
        private readonly IApiService _apiService;


        public UserViewModel()
        {
            IApiService apiService = new ApiService();
            _apiService = apiService;
            Traveler = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
            IsEnable = true;
        }

        public ImageSource ProfileImage
        {
            get { return _profileImage; }
            set { SetProperty(ref _profileImage, value); }
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
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }
        public TravelerResponse Traveler
        {
            get { return _traveler; }
            set { SetProperty(ref _traveler, value); }
        }

        public ICommand LoadImageCommand => new Command(LoadImageAsync);
        private async void LoadImageAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", "No camera available.", "OK");
                return;
            }

            var source = await Application.Current.MainPage.DisplayActionSheet("Get Picture from:", "Cancel", null, "From Gallery", "From Camera");
            if (source == "Cancel")
            {
                _file = null;
                return;
            }
            if (source == "From Camera")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Small,
                });
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }
            if (_file != null)
            {
                ProfileImage = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand UpdateCommand => new Command(UpdateAsync);
        private async void UpdateAsync()
        {
            IsEnable = false;
            var isValid = ValidateData();
            if (!isValid)
            {
                return;
            }

            var userRequest = new UserRequest
            {
                Email = Traveler.Email,
                FirstName = Traveler.FirstName,
                LastName = Traveler.LastName,
                Company = Traveler.Company,
                Password = "123456",
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.PutAsync(
                url,
                "/api",
                "/Account",
                userRequest,
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Settings.Traveler = JsonConvert.SerializeObject(Traveler);
            IsEnable = true;

            await App.Current.MainPage.DisplayAlert(
                "Update",
                "User updated succesfully.",
                "Accept");

            await Shell.Current.GoToAsync("//TripPage");
        }

        public ICommand PswChangeCommand => new Command(PswChange);
        private void PswChange()
        {
            Shell.Current.Navigation.PushAsync(new ChangePswPage());
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(Traveler.FirstName))
            {
                AlertDialog = "You must enter your First Name";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(Traveler.LastName))
            {
                AlertDialog = "You must enter your Last Name";
                EntryEmpty = "Red";
                return false;
            }

            if (string.IsNullOrEmpty(Traveler.Company))
            {
                AlertDialog = "You must enter your Company";
                EntryEmpty = "Red";
                return false;
            }
            return true;
        }
    }
}
