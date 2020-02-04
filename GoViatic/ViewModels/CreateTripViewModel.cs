using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class CreateTripViewModel : BaseViewModel
    {
        private decimal _budget;
        private DateTime _minDate;
        private DateTime _date;
        private DateTime _endDate;

        public CreateTripViewModel()
        {
            MinDate = DateTime.Today;
        }

        public decimal Budget
        {
            get { return _budget; }
            set { SetProperty(ref _budget, value); }
        }

        public DateTime Date
        {
            get { return _date;}
            set { SetProperty(ref _date, value); }
        }
        
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        public ICommand SaveCommand => new Command(Save);
        private void Save(object obj)
        {
            //TODO: SAVE CITY USING THE API SERVICE FOR TRIP SAVE
            Application.Current.MainPage.DisplayAlert("Mensaje", "Pendiente Implementar", "Ok");
        }
    }
}
