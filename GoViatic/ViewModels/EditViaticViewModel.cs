using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Common.Services;
using GoViatic.Helpers;
using GoViatic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Type", "type")]
    [QueryProperty("TripSelection", "tripid")]
    [QueryProperty("ViaticSelection", "viaticid")]
    public class EditViaticViewModel : BaseViewModel
    {
        private string _navTittle;
        private bool _isEnable;
        private bool _isRunning;
        private string _entryEmpty;
        private string _alertDialog;
        private string _tittle;
        private string _columnSpan;
        private string _deleteVisible;
        private string _saveColumnSpan;
        private ViaticResponse _viatic;
        private IApiService _apiService;
        private ViaticType _selectedViaticType;

        public EditViaticViewModel()
        {
            IsEnable = true;
            EntryEmpty = "Transparent";
            IApiService apiService = new ApiService();
            _apiService = apiService;
        }

        public bool IsEdit { get; set; }
        public int TripIdNumber { get; set; }
        public int ViaticIdNumber { get; set; }
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
        public IList<ViaticType> ViaticCat 
        { 
            get { return ViaticTypeData.ViaticsList; } 
        }
        public string SaveColumnSpan
        {
            get { return _saveColumnSpan; }
            set { SetProperty(ref _saveColumnSpan, value); }
        }
        public ViaticResponse Viatic
        {
            get { return _viatic; }
            set { SetProperty(ref _viatic, value); }
        }
        public string TripSelection
        {
            set
            {
                TripIdNumber = int.Parse(Uri.UnescapeDataString(value));
            }
        }
        public string ViaticSelection
        {
            set
            {
                ViaticIdNumber = int.Parse(Uri.UnescapeDataString(value));
                var currenttrip = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
                Viatic = currenttrip.Trips.FirstOrDefault(m => m.Id.ToString() == TripIdNumber.ToString()).Viatics.FirstOrDefault(v =>v.Id.ToString() == Uri.UnescapeDataString(value));
            }
        }
        public ViaticType SelectedViaticType
        {
            get { return _selectedViaticType; }
            set 
            {
                if (_selectedViaticType != value)
                {
                    _selectedViaticType = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Type
        {
            set
            {
                if (value == "Create")
                {
                    Viatic = new ViaticResponse {InvoiceDate = DateTime.Today };
                    NavTittle = Languages.NavTittleCreateViatic;
                    Tittle = Languages.CreateViaticTittle;
                    ColumnSpan = "2";
                    SaveColumnSpan = "2";
                    DeleteVisible = "False";
                }
                else
                {
                    NavTittle = Languages.NavTittleEditViatic;
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

            var viaticRequest = new ViaticRequest
            {
                Id = Viatic.Id,
                Name = Viatic.Name,
                Description = Viatic.Description,
                InvoiceAmmount = Viatic.InvoiceAmmount,
                InvoiceDate = Viatic.InvoiceDate,
                ViaticType = SelectedViaticType.Name,
                TripId = TripIdNumber,
                TravelerId = traveler.Id,
            };

            Response<object> response;
            if (IsEdit)
            {
                response = await _apiService.PutAsync(
                    url,
                    "/api",
                    "/Viatics",
                    viaticRequest.Id,
                    viaticRequest,
                    "bearer",
                    token.Token);
            }
            else
            {
                response = await _apiService.PostAsync(
                    url,
                    "/api",
                    "/Viatics",
                    viaticRequest,
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
               Languages.TripEditCreation,
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
                "/Viatics",
                ViaticIdNumber,
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
            await Application.Current.MainPage.DisplayAlert("Message", "Viatic has been deleted", "Accept");
            await Shell.Current.Navigation.PopAsync();
        }


        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(Viatic.Name))
            {
                AlertDialog = "Please insert a valid Viatic";
                EntryEmpty = "Red";
                return false;
            }
            return true;
        }
    }
}
