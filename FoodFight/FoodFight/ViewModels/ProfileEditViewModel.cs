using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodFight.ViewModels
{
    public class ProfileEditViewModel : ViewModelBase
    {
        INavigationService _navigationService;

        IDataService<User> _userRepo;
        User _mainUser;

        public User MainUser 
        {
            get => _mainUser;
            set => SetProperty(ref _mainUser, value);
        }

        public ProfileEditViewModel(INavigationService navigationService, IDataService<User> userRepo) : base(navigationService)
        {
            _navigationService = navigationService;
            _userRepo = userRepo;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            MainUser = parameters.GetValue<User>("MainUser");
        }
    }
}
