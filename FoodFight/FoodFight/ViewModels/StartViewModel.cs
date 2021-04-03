﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodFight.ViewModels
{
    public class StartViewModel : BindableBase
    {

        double _latitude;
        double _longitude;
        double _userInputLat;
        double _userInputLong;

        public double Latitude 
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public double UserInputLatitude
        {
            get => _userInputLat;
            set => SetProperty(ref _userInputLat, value);
        }

        public double UserInputLongitude
        {
            get => _userInputLong;
            set => SetProperty(ref _userInputLong, value);
        }


        public StartViewModel()
        {
            GetUserLocation();
        }

        private async void GetUserLocation()
        {
            CancellationTokenSource cts;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    await Application.Current.MainPage.DisplayAlert("User Location", location.ToString(), "Close");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
