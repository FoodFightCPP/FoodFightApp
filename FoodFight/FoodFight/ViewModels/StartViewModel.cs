using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodFight.ViewModels
{
    public class StartViewModel : ViewModelBase
    {

        double _latitude;
        double _longitude;
        double _userInputLat;
        double _userInputLong;

        IDataService<User> _userRepo;

        User _mainUser;

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


        public StartViewModel(IDataService<User> userRepo, INavigationService navigationService) : base (navigationService)
        {
            _userRepo = userRepo;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            _mainUser = parameters.GetValue<User>("MainUser");
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
                    _mainUser.Lat = location.Latitude.ToString();
                    _mainUser.Lng = location.Longitude.ToString();
                    await _userRepo.Update(_mainUser.UserId, _mainUser, "Users");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
