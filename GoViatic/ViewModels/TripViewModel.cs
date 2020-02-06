using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class TripViewModel : BaseViewModel
    {
        private string _firstName;
        public static ICollection<TripResponse> trips;
        private bool _isRefreshing;
        private TripResponse _selection;
        private string _viaticCount;

        public TripViewModel()
        {
            IsRefreshing = false;
            GetUserData();
            
            EditCommand = new Command<TripResponse>((t) =>
            {
                var id = t.Id;
                Routing.RegisterRoute("TripPage/EditTripPage", typeof(EditTripPage));
                Shell.Current.GoToAsync($"//TripPage/EditTripPage?id={id}");
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
        public ICollection<TripResponse> Trips 
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
            FirstName = $"{userData.FirstName} {userData.LastName} Choose your Trip";
            Trips = userData.Trips;
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
        private void Create()
        {
            Shell.Current.Navigation.PushAsync(new CreateTripPage(),false);
        }
    }
}
