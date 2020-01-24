using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Data;
using GoViatic.Models;
using GoViatic.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Trips", "id")]
    public class ViaticViewModel : BaseViewModel
    {
        private IList<ViaticT> _viaticsType;
        private string _tittle;
        private string _city;
        private string _viaticName;
        private ICollection<ViaticResponse> _viatics;
        private bool _isRefreshing;
        private ViaticResponse _selection;

        public ViaticViewModel()
        {
            IsRefreshing = false;
            ViaticsType = ViaticsData.Viatics;
        }
        public Command<ViaticResponse> EditCommand { get; set; }

        public string Trips
        {
            set
            {

                var allTrips = TripViewModel.trips;
                TripResponse trip = allTrips.FirstOrDefault(t => t.Id.ToString() == Uri.UnescapeDataString(value));
                if (trip != null)
                {
                    City = trip.City;
                    Tittle = $"Viatics for {City}";
                    Viatics = trip.Viatics;
                    Id = value;
                }
            }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public ICollection<ViaticResponse> Viatics 
        { 
            get { return _viatics;}
            set { SetProperty(ref _viatics, value); }
        }

        public string Id { get; private set; }

        public IList<ViaticT> ViaticsType
        {
            get { return _viaticsType; }
            set { SetProperty(ref _viaticsType, value); }
        }

        public string Tittle
        {
            get { return _tittle; }
            set { SetProperty(ref _tittle, value); }
        }

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        public string ViaticName
        {
            get { return _viaticName; }
            set { SetProperty(ref _viaticName, value); }
        }

        public ViaticResponse Selection
        {
            get { return _selection; }
            set { SetProperty(ref _selection, value); }
        }

        public ICommand RefreshCommand => new Command(Refresh);
        private void Refresh()
        {
            IsRefreshing = true;
            GetData();
            IsRefreshing = false;
        }

        public ICommand SelectionCommand => new Command(SelectionC);
        private async void SelectionC()
        {
            if (Selection != null)
            {
                var name = Selection.Name;
                
                Routing.RegisterRoute("EditViaticPage", typeof(EditViaticPage));
                await Shell.Current.GoToAsync("//ViaticsPage/EditViaticPage");
                Selection = null;
            }
        }

        public async void GetData()
        {
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = LoginViewModel.token;
            var email = LoginViewModel._email;
            IApiService apiService = new ApiService();
            var response2 = await apiService.GetTravelerByEmail(url, "api", "/Travelers/GetTravelerByEmail", "bearer", token, email);
            var traveler = (TravelerResponse)response2.Result;
            var trips = traveler.Trips;
            var currenttrip = trips.FirstOrDefault(m => m.Id == Int32.Parse(Id));
            Viatics = currenttrip.Viatics;
        }
    }
}
