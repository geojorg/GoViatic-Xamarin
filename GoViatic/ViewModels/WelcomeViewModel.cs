using GoViatic.Common.Helpers;
using GoViatic.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        readonly IList<Carousel> source;
        private string _boxView0;
        private string _boxView1;
        private string _boxView2;

        public ObservableCollection<Carousel> Carousels { get; private set; }
        public int CurrentPosition { get; set; }
        public string BoxView0
        {
            get { return _boxView0; }
            set { SetProperty(ref _boxView0, value); }
        }
        public string BoxView1
        {
            get { return _boxView1; }
            set { SetProperty(ref _boxView1, value); }
        }
        public string BoxView2
        {
            get { return _boxView2; }
            set { SetProperty(ref _boxView2, value); }
        }

        public WelcomeViewModel()
        {
            Settings.FirstRun = false;
            BoxView0 = "Accent";
            BoxView1 = "#C1C0C0";
            BoxView2 = "#C1C0C0";
            source = new List<Carousel>();
            CreateCarouselCollection();
            OnPropertyChanged("CurrentItem");
        }

        private void CreateCarouselCollection()
        {
            source.Add(new Carousel
            {
                Icon = "ic_goviatic",
                Header = "Welcome to GoViatic",
                Body = "Make control of your viatics easy as you travel."
            });
            source.Add(new Carousel
            {
                Icon = "carousel1",
                Header = "Simple Management",
                Body = "Manage your transportation, food and many other expenses"
            });
            source.Add(new Carousel
            {
                Icon = "carousel2",
                Header = "Export the Summary",
                Body = "Export the summary of your invoices viatics in Excel or PDF"
            });
            Carousels = new ObservableCollection<Carousel>(source);
        }

        public ICommand PositionChangedCommand => new Command<int>(PositionChanged);
        private void PositionChanged(int position)
        {
            CurrentPosition = position;
            OnPropertyChanged("CurrentPosition");
            switch (position)
            {
                case 0:
                    BoxView0 = "Accent";
                    BoxView1 = "#C1C0C0";
                    BoxView2 = "#C1C0C0";
                    break;
                case 1:
                    BoxView0 = "#C1C0C0";
                    BoxView1 = "Accent";
                    BoxView2 = "#C1C0C0";
                    break;
                case 2:
                    BoxView0 = "#C1C0C0";
                    BoxView1 = "#C1C0C0";
                    BoxView2 = "Accent";
                    break;
            }
        }
       
        public ICommand LoginCommand => new Command(Login);
        private void Login()
        {
            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
