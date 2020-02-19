using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Trips", "id")]
    [QueryProperty("Type", "type")]
    public class EditTripViewModel : BaseViewModel
    {
        private bool _isEnable;
        private bool _isRunning;
        private string _entryEmpty;
        private string _alertDialog;
        private string _tittle;
        private string _columnSpan;
        private string _navTittle;
        private string _deleteVisible;
        private string _saveColumnSpan;
        private TripResponse _trip;
        private readonly IApiService _apiService;

        public EditTripViewModel()
        {
            IsEnable = true;
            EntryEmpty = "Transparent";
            IApiService apiService = new ApiService();
            _apiService = apiService;
        }

        public bool IsEdit { get; set; }
        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }
        public bool IsRunning
        {
            get { return _isRunning; }
            set { SetProperty(ref _isRunning, value); }
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
        public string NavTittle
        {
            get { return _navTittle; }
            set { SetProperty(ref _navTittle, value); }
        }
        public string Tittle
        {
            get { return _tittle; }
            set { SetProperty(ref _tittle, value); }
        }
        public string ColumnSpan
        {
            get { return _columnSpan; }
            set { SetProperty(ref _columnSpan, value); }
        }
        public string DeleteVisible
        {
            get { return _deleteVisible; }
            set { SetProperty(ref _deleteVisible, value); }
        }
        public string SaveColumnSpan
        {
            get { return _saveColumnSpan; }
            set { SetProperty(ref _saveColumnSpan, value); }
        }
        public TripResponse Trip
        {
            get { return _trip; }
            set { SetProperty(ref _trip, value); }
        }

        public ICollection<ViaticResponse> Viatics { get; private set; }

        public string Trips
        {
            set
            {
                var allTrips = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
                Trip = allTrips.Trips.FirstOrDefault(m => m.Id.ToString() == Uri.UnescapeDataString(value));
            }
        }

        public string Type
        {
            set
            {
                if (value == "Create")
                {
                    Trip = new TripResponse { Date = DateTime.Today, EndDate = DateTime.Today.AddDays(2) };
                    NavTittle = Languages.NavTittleCreateTrip;
                    Tittle = Languages.CreateTripTittle;
                    ColumnSpan = "2";
                    SaveColumnSpan = "2";
                    DeleteVisible = "False";
                }
                else
                {
                    NavTittle = Languages.NavTittleEditTrip;
                    Tittle = Languages.EditViaticTittle;
                    ColumnSpan = "2";
                    DeleteVisible = "True";
                    SaveColumnSpan = "1";
                    IsEdit = true;
                }
            }
        }


        public ICommand SaveCommand => new Command(SaveAsync);
        private async void SaveAsync()
        {
            EntryEmpty = "Transparent";
            var isValid = ValidateData();
            if (!isValid)
            {
                return;
            }

            IsEnable = false;
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var traveler = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);

            var tripRequest = new TripRequest
            {
                Id = Trip.Id,
                City = Trip.City,
                Budget = Trip.Budget,
                Date = Trip.Date,
                EndDate = Trip.EndDate,
                TravelerId = traveler.Id,
            };

            Response<object> response;
            if (IsEdit)
            {
                response = await _apiService.PutAsync(
                    url, 
                    "/api", 
                    "/Trips", 
                    tripRequest.Id,
                    tripRequest, 
                    "bearer", 
                    token.Token);
            }
            else
            {
                response = await _apiService.PostAsync(
                    url, 
                    "/api", 
                    "/Trips", 
                    tripRequest, 
                    "bearer", 
                    token.Token);
            }

            IsEnable = true;
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            await App.Current.MainPage.DisplayAlert(
               Languages.Accept,
               string.Format(Languages.CreateEditTripConfirm, IsEdit ? Languages.Edited : Languages.Created),
               Languages.Accept);
            await Shell.Current.Navigation.PopAsync();
            await TripViewModel.GetInstance().UpdateUserData();
        }

        public ICommand DeleteCommand => new Command(AsyncDelete);
        private async void AsyncDelete()
        {
            var answer = await App.Current.MainPage.DisplayAlert(
                Languages.Confirm,
                Languages.QuestionV,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;
            }

            IsEnable = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await _apiService.DeleteAsync(
                url, 
                "/api", 
                "/Trips", 
                Trip.Id, 
                "bearer", 
                token.Token);

            if (!response.IsSuccess)
            {
                IsEnable = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            IsEnable = true;
            await TripViewModel.GetInstance().UpdateUserData();
            await Application.Current.MainPage.DisplayAlert("Message", "Trips has been deleted", "Accept");
            await Shell.Current.Navigation.PopAsync();
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(Trip.City))
            {
                AlertDialog = "Please insert a valid City";
                EntryEmpty = "Red";
                return false;
            }
            return true;
        }
    }
}
