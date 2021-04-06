using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

        public DelegateCommand<string> ViewFacebookCommand { get; set; }

        public ProfileViewModel(IDataService<User> profileRepo, INavigationService navigationService) : base(navigationService)
        {
            _profileRepo = profileRepo;
            EditProfileCommand = new DelegateCommand(EditProfile);
            _navigationService = navigationService;
            LogOutCommand = new DelegateCommand(LogOut);
            Uri Facebook = new Uri("https://facebook.com/Peter.B.Steele");
            ViewFacebookCommand = new DelegateCommand<string>(OpenFacebook);
        }

        private async void OpenFacebook(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception)
            {

                throw;
            }
        }

        async void EditProfile()
        {
            var user = new NavigationParameters
                    {
                        { "MainUser", AppUser }
                    };
            await _navigationService.NavigateAsync("ProfileEdit", user);
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

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            AppUser = await _profileRepo.Get(AppUser.UserId, "Users");
        }
    }
}
