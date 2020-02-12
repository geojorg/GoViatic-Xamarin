using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    //TODO: CHECK IF THE USER HAS TRIPS - UPDATE TO RETRIEVE INFORMATION FROM THE JSON AND NOT FROM THE MODEL.
    public class TripViewModel : BaseViewModel
    {
        private string _firstName;
        private bool _isRefreshing;
        private TripResponse _selection;
        private string _viaticCount;
        private ObservableCollection<TripModel> trips;

        public TripViewModel()
        {
            IsRefreshing = false;
            GetUserData();
            
            EditCommand = new Command<TripResponse>(async (t) =>
            {
                var id = t.Id;
                var type = "Edit";
                Routing.RegisterRoute("TripPage/EditTripPage", typeof(EditTripPage));
                await Shell.Current.GoToAsync($"//TripPage/EditTripPage?type={type}&id={id}");
            });
        }

        public bool HasTrips { get; set; }

        public bool FirstTrip { get; set; }

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
        public string ViaticCount
        {
            get { return _viaticCount; }
            set { SetProperty(ref _viaticCount, value); }
        }
        
        private void GetUserData()
        {
            var userData = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
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

            FirstName = $"{userData.FirstName} {userData.LastName} Choose your Trip";
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
        public void Refresh()
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
