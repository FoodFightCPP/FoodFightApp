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
    public class ProfileViewModel : BindableBase
    {
        readonly IDataService<User> _profileRepo;
        User _appUser;

        public User AppUser 
        {
            get => _appUser;
            set => SetProperty(ref _appUser, value);
        }


        public ProfileViewModel(IDataService<User> profileRepo)
        {
            _profileRepo = profileRepo;
            GetProfileInformation();
        }

        private async void GetProfileInformation()
        {
            Guid mainUser = new Guid("3477E6CF-53DA-48D9-B6EA-3ECB7CF879FC");
            AppUser = await _profileRepo.Get(mainUser, "Users");
        }
    }
}
