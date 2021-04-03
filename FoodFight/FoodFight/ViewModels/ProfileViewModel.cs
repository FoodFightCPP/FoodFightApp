using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace FoodFight.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        readonly IDataService<User> _profileRepo;
        INavigationService _navigationService;
        User _appUser;

        public User AppUser 
        {
            get => _appUser;
            set => SetProperty(ref _appUser, value);
        }

        public DelegateCommand EditProfileCommand { get; set; }
        public DelegateCommand LogOutCommand { get; set; }

        public ProfileViewModel(IDataService<User> profileRepo, INavigationService navigationService) : base(navigationService)
        {
            _profileRepo = profileRepo;
            EditProfileCommand = new DelegateCommand(EditProfile);
            _navigationService = navigationService;
            LogOutCommand = new DelegateCommand(LogOut);
        }

        async void EditProfile()
        {
            await _navigationService.NavigateAsync("ProfileEdit");
        }

        async void LogOut()
        {
            var response = await Application.Current.MainPage.DisplayAlert("Log Out", "Are you sure you wish to Logout?", "Yes", "No");
            if (response.Equals(true))
            {
                await _navigationService.NavigateAsync("/SimpleLoginPage");
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            AppUser = parameters.GetValue<User>("MainUser");
        }
    }
}
