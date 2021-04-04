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

        public DelegateCommand UpdateProfileCommand { get; set; }
        public DelegateCommand CancelUpdateCommand { get; set; }

        public User MainUser 
        {
            get => _mainUser;
            set => SetProperty(ref _mainUser, value);
        }

        public ProfileEditViewModel(INavigationService navigationService, IDataService<User> userRepo) : base(navigationService)
        {
            _navigationService = navigationService;
            _userRepo = userRepo;
            UpdateProfileCommand = new DelegateCommand(UpdateProfile);
            CancelUpdateCommand = new DelegateCommand(CancelUpdate);
        }

        private async void CancelUpdate()
        {
            await _navigationService.GoBackAsync();
        }

        private async void UpdateProfile()
        {
            await _userRepo.Update(MainUser.UserId, MainUser, "Users");
            await _navigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            MainUser = parameters.GetValue<User>("MainUser");
        }
    }
}
