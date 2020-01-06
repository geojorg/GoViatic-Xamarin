using GoViatic.Common.Models;
using GoViatic.Common.Services;
using System.Collections.Generic;
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
        private string _firstName;
        private string _city;
        private ICollection<TripResponse> _trips;

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
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        public ICollection<TripResponse> Trips 
        { 
            get { return _trips; }  
            set { SetProperty(ref _trips, value); } 
        }

        private async void GetUserData()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response2 = await _apiService.GetTravelerByEmail(url, "api", "/Travelers/GetTravelerByEmail", "bearer", token, email);
            var traveler = (TravelerResponse)response2.Result;
            FirstName = traveler.FirstName;
            Trips = traveler.Trips;
        }
    }
}
