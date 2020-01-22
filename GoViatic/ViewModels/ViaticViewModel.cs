﻿using GoViatic.Common.Models;
using GoViatic.Data;
using GoViatic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GoViatic.ViewModels
{
    [QueryProperty("Trips", "id")]
    public class ViaticViewModel : BaseViewModel
    {
        private IList<ViaticT> _viaticsType;
        private string _tittle;
        private string _city;
        private string _viaticName;
        private ICollection<ViaticResponse> _viatics;

        public ViaticViewModel()
        {
            ViaticsType = ViaticsData.Viatics;
        }

        public string Trips
        {
            set
            {
                var allTrips = TripViewModel.trips;
                TripResponse trip = allTrips.FirstOrDefault(t => t.Id.ToString() == Uri.UnescapeDataString(value));
                if (trip != null)
                {
                    City = trip.City;
                    Tittle = $"Viatics for {City}";
                    Viatics = trip.Viatics;
                }
            }
        }

        public ICollection<ViaticResponse> Viatics 
        { 
            get { return _viatics;}
            set { SetProperty(ref _viatics, value); }
        }

        public IList<ViaticT> ViaticsType
        {
            get { return _viaticsType; }
            set { SetProperty(ref _viaticsType, value); }
        }

        public string Tittle
        {
            get { return _tittle; }
            set { SetProperty(ref _tittle, value); }
        }

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        public string ViaticName
        {
            get { return _viaticName; }
            set { SetProperty(ref _viaticName, value); }
        }
    }
}
