using GoViatic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    //TODO:replace with the model one the new system is in place.
    [QueryProperty("Trips", "id")]
    public class EditTripViewModel : BaseViewModel
    {
        private string _city;
        private decimal _budget;
        private DateTime _date;
        private DateTime _endDate;

        public string Trips
        {
            set
            {
                var allTrips = TripViewModel.trips;
                TripResponse trip = allTrips.FirstOrDefault(m => m.Id.ToString() == Uri.UnescapeDataString(value));
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

        public ICollection<ViaticResponse> Viatics { get; private set; }

        public decimal Budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value); }  
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
