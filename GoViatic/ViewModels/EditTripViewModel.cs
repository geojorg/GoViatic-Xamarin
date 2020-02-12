using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
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
        private string _city;
        private decimal _budget;
        private DateTime _date;
        private DateTime _endDate;
        private string _tittle;
        private string _columnSpan;
        private string _navTittle;
        private string _deleteVisible;
        private string _saveColumnSpan;

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
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }
        public decimal Budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value); }
        }

        public ICollection<ViaticResponse> Viatics { get; private set; }
        

        // TODO:PENDING TO SOLVE BECAUSE OF A MODEL CHANGE
        public string Trips
        {
            set
            {
                var allTrips = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
                TripResponse trip = allTrips.Trips.FirstOrDefault(m => m.Id.ToString() == Uri.UnescapeDataString(value));
                if (trip != null)
                {
                    City = trip.City;
                    Budget = trip.Budget;
                    Date = trip.Date;
                    EndDate = trip.EndDate;
                    Viatics = trip.Viatics;
                }
            }
        }

        public string Type
        {
            set
            {
                if (value == "Create")
                {
                    NavTittle = "Create Trip";
                    Tittle = "Let's Create your new Trip";
                    ColumnSpan = "2";
                    SaveColumnSpan = "2";
                    DeleteVisible = "False";
                }
                else
                {
                    NavTittle = "Edit Trip";
                    Tittle = "Edit your Trip";
                    ColumnSpan = "2";
                    DeleteVisible = "True";
                    SaveColumnSpan = "1";
                }
            }
        }


        public ICommand SaveCommand => new Command(Save);
        private void Save()
        {
            //TODO: GET THE THE SAVE COMMAND DONE
            Application.Current.MainPage.DisplayAlert("Mensaje", "Pendiente Implementar", "Ok");
        }

        public ICommand DeleteCommand => new Command(Delete);

        private void Delete()
        {
            //Todo: GET THE DELETE COMMAND DONE
            Application.Current.MainPage.DisplayAlert("Mensaje", "Pendiente Implementar", "Ok");
        }
    }
}
