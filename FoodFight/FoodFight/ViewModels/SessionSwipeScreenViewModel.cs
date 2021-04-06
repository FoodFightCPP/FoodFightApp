using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Navigation;

namespace FoodFight.ViewModels
{
    public class SessionSwipeScreenViewModel : ViewModelBase
    {
        INavigationService _navigationService;
        private IDataService<MatchSession> _matchSessionRepo;
        IGooglePlacesAPI<Restaurant> _gpaRestaurant;
        ObservableCollection<Result> _individualRestaurants;
        Restaurant _restaurants;
        User _mainUser;
        private MatchSession _matchSession;

        public ObservableCollection<Result> IndividualRestaurants
        {
            get => _individualRestaurants;
            set => SetProperty(ref _individualRestaurants, value);
        }

        public Restaurant Restaurants
        {
            get => _restaurants;
            set => SetProperty(ref _restaurants, value);
        }

        public User MainUser
        {
            get => _mainUser;
            set => SetProperty(ref _mainUser, value);
        }

        public MatchSession CurrentMatchSession
        {
            get => _matchSession;
            set => SetProperty(ref _matchSession, value);
        }

        public DelegateCommand SwipedLeftCommand { get; set; }
        public DelegateCommand SwipedRightCommand { get; set; }



        public SessionSwipeScreenViewModel(INavigationService navigationService, IGooglePlacesAPI<Restaurant> gpaRestaurant, IDataService<MatchSession> matchSessionRepo) : base (navigationService)
        {
            _navigationService = navigationService;
            _gpaRestaurant = gpaRestaurant;
            _matchSessionRepo = matchSessionRepo;

            SwipedLeftCommand = new DelegateCommand(RejectedRestaurants);
            SwipedRightCommand = new DelegateCommand(AcceptedRestaurants);
        }

        private async void AcceptedRestaurants()
        {
            // Create Accepted Restaurant Here
        }

        private void RejectedRestaurants()
        {
            // Create Rejected 
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            MainUser = parameters.GetValue<User>("MainUser");
            CurrentMatchSession = parameters.GetValue<MatchSession>("MatchSession");
            GetRestaurants();
        }

        private async void GetRestaurants()
        {
            
            Restaurants = await _gpaRestaurant.GetAllLocations(CurrentMatchSession.Lat, CurrentMatchSession.Lng, 15000);

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
