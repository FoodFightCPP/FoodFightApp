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
    public class RequestPermissionViewModel : ViewModelBase
    {
        INavigationService navigationService;
        IDataService<User> _userRepo;

        public Location UserLocation { get; set; }

        public User MainUser { get; set; }

        public DelegateCommand GrantPermissionCommand { get; set; }

        public RequestPermissionViewModel(INavigationService navigationService, IDataService<User> userRepo) : base(navigationService)
        {
            this.navigationService = navigationService;
            _userRepo = userRepo;
            GrantPermissionCommand = new DelegateCommand(GetUserPermission);
        }

        private async void GetUserPermission()
        {
            CancellationTokenSource cts;

            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }
            }
            catch (PermissionException exp)
            {
                await Application.Current.MainPage.DisplayAlert("Location Denied", "You have denied to allow the app to use your location. In order to use this application, you will now need to supply a zipcode to search around or renable this permission in: \n Settings -> Applications -> FoodFight -> Permissions -> Allow Location Permission" + exp, "Close");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Location Not Found", "We were unable to access your location. Please report this error to the devs: \n" + ex, "Close");
            }


            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            UserLocation = await Geolocation.GetLocationAsync(request, cts.Token);

            if (UserLocation != null)
            {
                MainUser.Lat = UserLocation.Latitude.ToString();
                MainUser.Lng = UserLocation.Longitude.ToString();
            }

            await _userRepo.Update(MainUser.UserId, MainUser, "Users");

            var navigationParmeters = new NavigationParameters()
            {
                {"MainUser", MainUser }
            };

            await navigationService.NavigateAsync("SessionLocation", navigationParmeters);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            MainUser = parameters.GetValue<User>("MainUser");
        }

    }
}
