using GoViatic.Common.Models;
using System;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    
    public class EditTripViewModel : BaseViewModel
    {
        private string _city;
        private DateTime _date;
        private DateTime _endDate;
        private decimal _budget;
        private int _id;
        private TripResponse _data;

        public EditTripViewModel()
        {
            Budget = Data.Budget;
            City = Data.City;
            Date = Data.Date;
            EndDate = Data.EndDate;
        }

        public TripResponse Data
        {
            get { return _data; }
            set { SetProperty(ref _data, value); }
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
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

    }
}
