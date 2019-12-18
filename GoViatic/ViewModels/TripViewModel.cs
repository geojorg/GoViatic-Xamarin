using GoViatic.Common.Models;
using GoViatic.Common.Services;
using System;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Token", "token")]
    public class TripViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public TripViewModel()
        {
            IApiService apiService = new ApiService();
            _apiService = apiService;
            var check = Token;
            GetUserData();
        }

        private string _token;
        public string Token
        {
            set { _token = Uri.UnescapeDataString(value); }
            get { return _token; }
        }


        private async void GetUserData()
        {
            Console.WriteLine(_token);
            Console.WriteLine(Token);
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response2 = await _apiService.GetTravelerByEmail(url, "api", "/Travelers/GetTravelerByEmail", "bearer", Token, "jgm@gmail.com");
            var traveler = (TravelerResponse)response2.Result;
        }


       
    }
}
