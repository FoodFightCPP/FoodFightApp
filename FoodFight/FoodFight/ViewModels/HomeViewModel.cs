using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace FoodFight.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        readonly IDataService<User> _dataService;
        IGooglePlacesAPI<Restaurant> _gpaRestaurant;
        ObservableCollection<User> _appUser;
        Restaurant _restaurants;
        ObservableCollection<Result> _individualRestaurants;
        User _mainUser;

        public ObservableCollection<Result> IndividualRestaurants
        {
            get => _individualRestaurants;
            set => SetProperty(ref _individualRestaurants, value);
        }

        public User MainUser 
        {
            get => _mainUser;
            set => SetProperty(ref _mainUser, value);
        }

        public Restaurant Restaurants
        {
            get => _restaurants;
            set => SetProperty(ref _restaurants, value);
        }

        public HomeViewModel(IDataService<User> dataService, IGooglePlacesAPI<Restaurant> gpaRestaurant, INavigationService navigationService) : base(navigationService)
        {
            _dataService = dataService;
            _gpaRestaurant = gpaRestaurant;
        }

        public override void Initialize(INavigationParameters parameters)
        {
            MainUser = parameters.GetValue<User>("MainUser");
            GetRestaurants();
        }

        private async void GetRestaurants()
        {
            Restaurants = await _gpaRestaurant.GetAllLocations(MainUser.Lat, MainUser.Lng, 15000);

            IndividualRestaurants = new ObservableCollection<Result>();

            foreach (var item in Restaurants.Results)
            {

                var res = new Result()
                {
                    Name = item.Name,
                    BusinessStatus = item.BusinessStatus,
                    Geometry = item.Geometry,
                    Icon = item.Icon,
                    OpeningHours = item.OpeningHours,
                    Photos = item.Photos,
                    PlaceId = item.PlaceId,
                    PlusCode = item.PlusCode,
                    Rating = item.Rating,
                    Reference = item.Reference,
                    Scope = item.Scope,
                    Types = item.Types,
                    UserRatingsTotal = item.UserRatingsTotal,
                    Vicinity = item.Vicinity
                };

                IndividualRestaurants.Add(res);
            }
         }
    }
}
