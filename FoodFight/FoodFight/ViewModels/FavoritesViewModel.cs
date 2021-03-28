using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FoodFight.ViewModels
{
    public class FavoritesViewModel : BindableBase
    {
        IDataService<FavoriteRestaurant> _favRestaurant { get; set; }
        ObservableCollection<FavoriteRestaurant> _favoriteRestaurant;

        public ObservableCollection<FavoriteRestaurant> FavoriteRestaurants
        {
            get => _favoriteRestaurant;
            set => SetProperty(ref _favoriteRestaurant, value);
        }

        public FavoritesViewModel(IDataService<FavoriteRestaurant> favRestaurantRepo)
        {
            _favRestaurant = favRestaurantRepo;
            GetFavoriteRestaurants();
        }

        private async void GetFavoriteRestaurants()
        {
            FavoriteRestaurants = new ObservableCollection<FavoriteRestaurant>(await _favRestaurant.GetAll("FavoriteRestaurants"));
        }
    }
}
