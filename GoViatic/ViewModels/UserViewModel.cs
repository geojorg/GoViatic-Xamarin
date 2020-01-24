using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class UserViewModel : BaseViewModel
    {

        private string _name;
        private MediaFile _file;
        private ImageSource _profileImage;

        public UserViewModel()
        {
            ProfileImage = "ic_camera_alt";
        }

        public ImageSource ProfileImage 
        { 
            get { return _profileImage;}
            set { SetProperty(ref _profileImage, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
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
    }
}
