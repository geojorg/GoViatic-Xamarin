using GoViatic.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
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
            BoxView0 = "#247D4D";
            BoxView1 = "#C1C0C0";
            BoxView2 = "#C1C0C0";
            source = new List<Carousel>();
            CreateCarouselCollection();
            OnPropertyChanged("CurrentItem");
            Preferences.Set("firstRun", "Yes");
        }

        private void CreateCarouselCollection()
        {
            source.Add(new Carousel
            {
                Icon = "carousel0",
                Header = "Bienvenido a GoViatic",
                Body = "Realiza el control de tus gatos de viáticos fácil"
            });
            source.Add(new Carousel
            {
                Icon = "carousel1",
                Header = "Gestión de forma simple",
                Body = "Gestiona tus gastos de transporte, alimentación y muchos otros"
            });
            source.Add(new Carousel
            {
                Icon = "carousel2",
                Header = "Exporta el resumen",
                Body = "Exporta el resumen de tus facturas y viáticos en Excel o PDF"
            });
            Carousels = new ObservableCollection<Carousel>(source);
        }

        public ICommand PositionChangedCommand => new Command<int>(PositionChanged);
        void PositionChanged(int position)
        {
            CurrentPosition = position;
            OnPropertyChanged("CurrentPosition");
            switch (position)
            {
                case 0:
                    BoxView0 = "#247D4D";
                    BoxView1 = "#C1C0C0";
                    BoxView2 = "#C1C0C0";
                    break;
                case 1:
                    BoxView0 = "#C1C0C0";
                    BoxView1 = "#247D4D";
                    BoxView2 = "#C1C0C0";
                    break;
                case 2:
                    BoxView0 = "#C1C0C0";
                    BoxView1 = "#C1C0C0";
                    BoxView2 = "#247D4D";
                    break;
            }
        }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(() =>
                { Shell.Current.GoToAsync("//Login"); });
            }
        }
    }
}
