using GoViatic.Common.Models;
using GoViatic.Common.Services;
using System.Linq;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Token", "token")]
    [QueryProperty("Email", "email")]
    public class TripViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private string email;
        private string _email;
        private string token;
        private string _token;
        public static string FirstName;
        public static int TripCount;

        public TripViewModel()
        {
            IApiService apiService = new ApiService();
            _apiService = apiService;
        }

        public string Email 
        { 
            get { return _email; } 
            set
            {
                SetProperty(ref _email, value);
                email = Email;
                GetUserData();
            }
        }
        public string Token
        {
            get { return _token; }
            set 
            { 
                SetProperty(ref _token, value);
                token = Token;
            }
        }

        private async void GetUserData()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response2 = await _apiService.GetTravelerByEmail(url, "api", "/Travelers/GetTravelerByEmail", "bearer", token, email);
            var traveler = (TravelerResponse)response2.Result;
            FirstName = traveler.FirstName;
            TripCount = traveler.Trips.Count();
        }
    }
}
