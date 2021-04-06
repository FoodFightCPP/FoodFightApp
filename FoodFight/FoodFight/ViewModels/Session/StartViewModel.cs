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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodFight.ViewModels
{
    public class StartViewModel : ViewModelBase
    {

        #region Fields

        double _latitude;
        double _longitude;
        double _userInputLat;
        double _userInputLong;

        IDataService<User> _userRepo;
        IDataService<ConnectedUser> _contactRepo;

        User _mainUser;
        User _selectedContact;
        ObservableCollection<ConnectedUser> _contacts;
        INavigationService _navigationService;

        #endregion

        #region Properties

        public User MainUser
        {
            get => _mainUser;
            set => SetProperty(ref _mainUser, value);
        }

        public User SelectedContact
        {
            get => _selectedContact;
            set => SetProperty(ref _selectedContact, value);
        }

        public ObservableCollection<ConnectedUser> Contacts
        {
            get => _contacts;
            set => SetProperty(ref _contacts, value);
        }

        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        public double UserInputLatitude
        {
            get => _userInputLat;
            set => SetProperty(ref _userInputLat, value);
        }

        public double UserInputLongitude
        {
            get => _userInputLong;
            set => SetProperty(ref _userInputLong, value);
        }

        #endregion

        #region DelegateCommands

        public DelegateCommand<User> SelectContactCommand { get; set; }

        public DelegateCommand UpdateContactCommand { get; set; }

        #endregion

        #region Constructor

        public StartViewModel(IDataService<User> userRepo, IDataService<ConnectedUser> contactRepo, INavigationService navigationService) : base(navigationService)
        {
            _contactRepo = contactRepo;
            _userRepo = userRepo;
            SelectContactCommand = new DelegateCommand<User>(SelectContact);
            _navigationService = navigationService;
            UpdateContactCommand = new DelegateCommand(GetUserContacts);
        }

        #endregion

        #region Methods
        // Change this to get just the ConnectedUser Id instead of the entire User
        private async void SelectContact(User mainUser)
        {
            SelectedContact = mainUser;
            var parameters = new NavigationParameters()
            {
                {"MainUser", new User() },
                {"Contact", new ConnectedUser() }
            };

            await _navigationService.NavigateAsync("SessionLocation", parameters);
        }

        private async void GetUserContacts()
        {
            Contacts = new ObservableCollection<ConnectedUser>(await _contactRepo.GetConnectedUserById(MainUser.UserId, "ConnectedUsers"));
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            MainUser = parameters.GetValue<User>("MainUser");
            GetUserContacts();
        }


        #endregion

    }
}
