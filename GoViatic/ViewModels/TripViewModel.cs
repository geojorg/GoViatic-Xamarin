using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
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
        private ICollection<TripResponse> _trips;
        private bool _hasTrips;
        private bool _firstTrip;
        private bool _isRefreshing;
        private TripResponse _selection;

        public TripViewModel()
        {
            IApiService apiService = new ApiService();
            _apiService = apiService;
            IsRefreshing = false;
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
        public ICollection<TripResponse> Trips 
        { 
            get { return _trips; }  
            set { SetProperty(ref _trips, value); } 
        }
        public bool HasTrips
        {
            get { return _hasTrips; }
            set { SetProperty(ref _hasTrips, value); }
        }
        public bool FirstTrip
        {
            get { return _firstTrip; }
            set { SetProperty(ref _firstTrip, value); }
        }
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        public TripResponse Selection
        {
            get { return _selection; }
            set { SetProperty(ref _selection, value); }
        }

        private async void GetUserData()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var response2 = await _apiService.GetTravelerByEmail(url, "api", "/Travelers/GetTravelerByEmail", "bearer", token, email);
            var traveler = (TravelerResponse)response2.Result;
            FirstName = $"{traveler.FirstName} Choose your Trip";
            Trips = traveler.Trips;
            if (Trips.Count() == 0)
            {
                FirstTrip = true;
                HasTrips = false;
            }
            else
            {
                FirstTrip = false;
                HasTrips = true;
            }
        }

        public ICommand RefreshCommand => new Command(Refresh);
        private void Refresh()
        {
            IsRefreshing = true;
            GetUserData();
            IsRefreshing = false;
        }

        public ICommand SelectedCommand => new Command(Selected);
        private async void Selected()
        {
            if (Selection != null)
            {
                var selected = Selection;
                Routing.RegisterRoute("TripPage/ViaticsPage", typeof(ViaticsPage));
                await Shell.Current.GoToAsync($"//TripPage/ViaticsPage?cardname={selected}");
                Selection = null;
            }
        }

        public ICommand CreateCommand => new Command(Create);
        private void Create()
        {
            Shell.Current.Navigation.PushAsync(new CreateTripPage());
        }
    }
}
