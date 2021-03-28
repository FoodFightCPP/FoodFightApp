using FoodFight.DAL;
using FoodFight.DAL.Services;
using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using FoodFight.ViewModels;
using FoodFight.ViewModels.Forms;
using FoodFight.Views;
using FoodFight.Views.Forms;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace FoodFight
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Device.SetFlags(new string[] { "Shapes_Experimental" });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDA2NTI3QDMxMzgyZTM0MmUzMEx4dUs5V2w0MHBHUzdndkZJV2RUUE9Ma01NQ2h5SVhQUEdFOHk2anRhT3c9");

            await NavigationService.NavigateAsync("SimpleLoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();


            containerRegistry.RegisterSingleton<IDataService<User>, GenericDataService<User>>();
            containerRegistry.RegisterSingleton<IDataService<Restaurant>, GenericDataService<Restaurant>>();
            containerRegistry.RegisterSingleton<IDataService<FavoriteRestaurant>, GenericDataService<FavoriteRestaurant>>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Profile, ProfileViewModel>();
            containerRegistry.RegisterForNavigation<Settings, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<Contacts, ContactsViewModel>();
            containerRegistry.RegisterForNavigation<Favorites, FavoritesViewModel>();
            containerRegistry.RegisterForNavigation<Home, HomeViewModel>();
            containerRegistry.RegisterForNavigation<Start, StartViewModel>();

            containerRegistry.RegisterForNavigation<SimpleLoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SimpleResetPasswordPage, ResetPasswordViewModel>();
            containerRegistry.RegisterForNavigation<SimpleSignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<SimpleForgotPasswordPage, ForgotPasswordViewModel>();
        }
    }
}
