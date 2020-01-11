using GoViatic.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    //TODO:replace with the model one the new system is in place.
    [QueryProperty("Trips", "id")]
    public class EditTripViewModel : BaseViewModel
    {
        private string _city;
        private DateTime _date;
        private DateTime _endDate;
        private decimal _budget;
        public ICollection<ViaticResponse> Viatics { get; private set; }
        public string Id { get; set; }

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

        public decimal Budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value); }  
        }
    }
}
