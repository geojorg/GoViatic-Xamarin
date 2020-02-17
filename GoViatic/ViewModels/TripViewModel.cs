using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Helpers;
using GoViatic.Models;
using GoViatic.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class TripViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private string _firstName;
        private TripResponse _selection;
        private string _viaticCount;
        private TravelerResponse _traveler;
        private ObservableCollection<TripModel> trips;
        private static TripViewModel _instance;
        private readonly IApiService _apiService;

        public TripViewModel()
        {
            IsRefreshing = false;
            _instance = this;
            IApiService apiService = new ApiService();
            _apiService = apiService;
            GetUserData();
            
            EditCommand = new Command<TripResponse>(async (t) =>
            {
                var id = t.Id;
                var type = "Edit";
                Routing.RegisterRoute("TripPage/EditTripPage", typeof(EditTripPage));
                await Shell.Current.GoToAsync($"//TripPage/EditTripPage?type={type}&id={id}");
            });
        }
        public static TripViewModel GetInstance()
        {
            return _instance;
        }

        public bool HasTrips { get; set; }

        public bool FirstTrip { get; set; }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        public Command<TripResponse> EditCommand { get; set; }

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        public ObservableCollection<TripModel> Trips 
        { 
            get { return trips; }  
            set { SetProperty(ref trips, value); } 
        }
       
        public TripResponse Selection
        {
            get { return _selection; }
            set { SetProperty(ref _selection, value); }
        }
        public string ViaticCount
        {
            get { return _viaticCount; }
            set { SetProperty(ref _viaticCount, value); }
        }
        
        private void GetUserData()
        {
            var userData = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
            if (userData==null)
            {
                FirstTrip = true;
                return;
            }
            else
            {
                Trips = new ObservableCollection<TripModel>(userData.Trips.Select(t => new TripModel()
                {
                    Id = t.Id,
                    City = t.City,
                    Budget = t.Budget,
                    Date = t.Date,
                    EndDate = t.EndDate,
                    ViaticCount = t.ViaticCount,
                    Viatics = t.Viatics
                }).ToList());

                FirstName = $"{userData.FirstName} {userData.LastName} {Languages.Choose}";
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
        }

        public async Task UpdateUserData()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var email = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler).Email;

            var response = await _apiService.GetTravelerByEmail(
                url,
                "/api",
                "/Travelers/GetTravelerByEmail",
                "bearer",
                token.Token,
                email);

            if (response.IsSuccess)
            {
                var traveler = response.Result;
                Settings.Traveler = JsonConvert.SerializeObject(traveler);
                _traveler = traveler;
                GetUserData();
            }
        }

        public ICommand RefreshCommand => new Command(AsyncRefresh);
        public async void AsyncRefresh()
        {
            IsRefreshing = true;
            await UpdateUserData();
            IsRefreshing = false;
        }

        public ICommand SelectedCommand => new Command(Selected);
        private async void Selected()
        {
            if (Selection != null)
            {
                var id = Selection.Id;
                Routing.RegisterRoute("TripPage/ViaticsPage", typeof(ViaticsPage));
                await Shell.Current.GoToAsync($"//TripPage/ViaticsPage?id={id}");
                Selection = null;
            }
        }

        public ICommand CreateCommand => new Command(Create);
        private async void Create()
        {
            var type = "Create";
            Routing.RegisterRoute("TripPage/EditTripPage", typeof(EditTripPage));
            await Shell.Current.GoToAsync($"//TripPage/EditTripPage?type={type}");
        }
    }
}
