using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Xamarin.Essentials.Location;

namespace FoodFight.ViewModels
{
    public class SessionLocationViewModel : ViewModelBase
    {

        #region Fields

        INavigationService _navigationService;
        IDataService<User> _userRepo;

        User _mainUser;
        ConnectedUser _connectedUsers;
        Location _userLocation;

        double _lat;
        double _lng;

        double _userInputLat;
        double _userInputLng;
        int _zipcode;
        bool GetUserLocationByPhone;
        int _connectedId;

        #endregion

        #region Properties

        public int ConnectedId
        {
            get => _connectedId;
            set => SetProperty(ref _connectedId, value);
        }

        public User MainUser
        {
            get => _mainUser;
            set => SetProperty(ref _mainUser, value);
        }

        public ConnectedUser ConnectedUsers
        {
            get => _connectedUsers;
            set => SetProperty(ref _connectedUsers, value);
        }

        public double UserInputLat
        {
            get => _userInputLat;
            set => SetProperty(ref _userInputLat, value);
        }

        public double UserInputLng
        {
            get => _userInputLng;
            set => SetProperty(ref _userInputLng, value);
        }

        public int Zipcode
        {
            get => _zipcode;
            set => SetProperty(ref _zipcode, value);
        }

        public Location UserLocation
        {
            get => _userLocation;
            set => SetProperty(ref _userLocation, value);
        }

        #endregion

        #region DelgateCommands

        public DelegateCommand StartMatchSessionCommand { get; set; }
        public DelegateCommand UsePhoneLocationCommand { get; set; }

        #endregion

        #region Constructor

        public SessionLocationViewModel(INavigationService navigationService, IDataService<User> userRepo) : base(navigationService)
        {
            _navigationService = navigationService;
            _userRepo = userRepo;
            StartMatchSessionCommand = new DelegateCommand(StartMatchSession);
            UsePhoneLocationCommand = new DelegateCommand(GetUserLocation);
        }

        #endregion

        #region Methods

        private void StartMatchSession()
        {
            CreateMatchSession(GetUserLocationByPhone);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            MainUser = parameters.GetValue<User>("MainUser");
            //ConnectedId = parameters.GetValue<int>("Contact");
        }

        private Task<IEnumerable<double>> GetUserLatLng()
        {
            //Need to do a web api call to Google Maps API to convert Zipcode to Lat and Lng
            //Setup Google Cloud Console to accept Google Maps API Calls and Get API Key
            return null;
        }

        private async void GetUserLocation()
        {
            var navigationParameters = new NavigationParameters()
            {
                {"MainUser", MainUser }
            };
            await _navigationService.NavigateAsync("RequestPermission", navigationParameters);
        }

        private async void CreateMatchSession(bool GetUserLocationByPhone)
        {
            //Create a new match session with the Lat and Lng and ConnectedUserId
            //Later Idea use Display Alert to accept or reject restaurants

            if (GetUserLocationByPhone)
            {
                //Update Main User First before calling this
                MatchSession matchSession = new MatchSession()
                {
                    ConnectedUserId = ConnectedUsers.ConnectedUserId,
                    Lat = MainUser.Lat,
                    Lng = MainUser.Lng,
                    DateTime = DateTime.Now
                };
            }
            else
            {
                MatchSession matchSession = new MatchSession()
                {
                    ConnectedUserId = ConnectedUsers.ConnectedUserId,
                    Lat = UserInputLat.ToString(),
                    Lng = UserInputLng.ToString(),
                    DateTime = DateTime.Now
                };
            }


            for (int i = 0; i < 10; i++)
            {

                await Application.Current.MainPage.DisplayAlert("Restaurant " + i, "Do you want " + i, "Yes", "No");

            }
            bool IsDone = true;
        }

        #endregion
    }
}
