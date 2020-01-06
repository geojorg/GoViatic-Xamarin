namespace GoViatic.ViewModels
{
    public class MenuViewModel: BaseViewModel
    {
        private string _firstName;
        private int _tripCount;

        public MenuViewModel()
        {
            FirstName = TripViewModel.FirstName;
            TripCount = TripViewModel.TripCount;
        }

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        public int TripCount
        {
            get { return _tripCount; }
            set { SetProperty(ref _tripCount, value); }
        }
    }
}
