using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodFight.ViewModels
{
    public class ContactsViewModel : ViewModelBase
    {

        IDataService<User> _userRepo;
        IDataService<ConnectedUser> _contactRepo;

        ObservableCollection<User> _users;
        
        ConnectedUser _contact;
        User _user;

        public ConnectedUser Contact 
        {
            get => _contact;
            set => SetProperty(ref _contact, value);
        }

        public User MainUser 
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ObservableCollection<User> AppUsers { 
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public DelegateCommand<int?> HamburgerCommand { get; private set; }




        public ContactsViewModel(IDataService<User> userRepo, IDataService<ConnectedUser> contactRepo, INavigationService navigationService) : base(navigationService)
        {
            _userRepo = userRepo;
            _contactRepo = contactRepo;

            TestRepo();
            //CreateContact();
            //UpdateContact();
            //DeleteUser();
            HamburgerCommand = new DelegateCommand<int?>(CreateConnectedUser);

        }
        
        private async void CreateConnectedUser(int? parameter)
        {
            var param = parameter;
            Contact = new ConnectedUser()
            {
                BaseUserId = MainUser.UserId,
                FriendUserId = (int)param
            };

            await _contactRepo.Create(Contact, "ConnectedUsers");
            await Application.Current.MainPage.DisplayAlert("Successful", "Contact has been added!", "Close");
        }

        public async void TestRepo()
        { 
            AppUsers = new ObservableCollection<User>( await _userRepo.GetAll("Users"));
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            MainUser = parameters.GetValue<User>("MainUser");
        }
    }
}
