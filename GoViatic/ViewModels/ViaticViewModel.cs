using GoViatic.Common.Helpers;
using GoViatic.Common.Models;
using GoViatic.Models;
using GoViatic.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("CurrentTrip", "id")]
    public class ViaticViewModel : BaseViewModel
    {
        private bool _isRefreshing;
        private string _navTittle;
        private IList<ViaticT> _viaticsType;
        private string _viaticName;
        private ViaticResponse _selection;
        private ICollection<ViaticResponse> _viatics;

        public ViaticViewModel()
        {
            IsRefreshing = false;
            //TODO: REFACTOR VIATYCSTYPE
            //ViaticsType = ViaticsData.Viatics;
        }
        public Command<ViaticResponse> EditCommand { get; set; }


        //TODO: ORGANIZE FOR THE NEW MODEL.

        public string CurrentTrip
        {
            set
            {
                var currentTrip = JsonConvert.DeserializeObject<TravelerResponse>(Settings.Traveler);
                Trip = currentTrip.Trips.FirstOrDefault(m => m.Id.ToString() == Uri.UnescapeDataString(value));
                NavTittle = $"{Trip.City} List of Viatics";
                Viatics = Trip.Viatics;
            }
        }

        public TripResponse Trip { get; private set; }
        public ICollection<ViaticResponse> Viatics
        {
            get { return _viatics; }
            set { SetProperty(ref _viatics, value); }
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        public string NavTittle
        {
            get { return _navTittle; }
            set { SetProperty(ref _navTittle, value); }
        }
        public IList<ViaticT> ViaticsType
        {
            get { return _viaticsType; }
            set { SetProperty(ref _viaticsType, value); }
        }
        public string ViaticName
        {
            get { return _viaticName; }
            set { SetProperty(ref _viaticName, value); }
        }
        public ViaticResponse Selection
        {
            get { return _selection; }
            set { SetProperty(ref _selection, value); }
        }

        public ICommand RefreshCommand => new Command(Refresh);
        private void Refresh()
        {
            //TODO CHANGE THE GET DATA BUT BE TAKE INTO ACCOUNT HOW TO DO  IT FOR THE NEW VIATICS
            IsRefreshing = true;
            //GetData();
            IsRefreshing = false;
        }

        //TODO: CHANGE THE NAME OR USE THE ID FOR CHANGING THE SELECCTION
        public ICommand SelectionCommand => new Command(SelectionC);
        private async void SelectionC()
        {
            if (Selection != null)
            {
                var id = Selection.Id;
                var type = "Edit";
                Routing.RegisterRoute("ViaticsPage/EditViaticPage", typeof(EditViaticPage));
                await Shell.Current.GoToAsync($"ViaticsPage/EditViaticPage?type={type}");
                Selection = null;
            }
        }

        public ICommand CreateCommand => new Command(Create);
        private async void Create()
        {
            var type = "Create";
            Routing.RegisterRoute("ViaticsPage/EditViaticPage", typeof(EditViaticPage));
            await Shell.Current.GoToAsync($"ViaticsPage/EditViaticPage?type={type}");
        }


    }
}
