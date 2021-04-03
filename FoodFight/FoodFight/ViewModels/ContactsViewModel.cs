using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodFight.ViewModels
{
    public class ContactsViewModel : BindableBase
    {

        IDataService<User> _userRepo { get; set; }
        IDataService<ConnectedUser> _contactRepo { get; set; }

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




        public ContactsViewModel(IDataService<User> userRepo, IDataService<ConnectedUser> contactRepo)
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
                BaseUserId = 6,
                FriendUserId = (int)param
            };

            await _contactRepo.Create(Contact, "ConnectedUsers");
        }

        private async void DeleteUser()        
        {
            bool success = await _userRepo.Delete(MainUser.UserId, "Users");
            if (success)
            {
               await Application.Current.MainPage.DisplayAlert("Success", "User Deleted from the DB", "Close");
            } else
            {
               await Application.Current.MainPage.DisplayAlert("Failure", "User NOT Deleted from the DB", "Close");
            }
            
        }

        private async void UpdateContact()
        {
            MainUser.Name = "Billy Bob";
            MainUser.Bio = "This is a dumb thing to say here";
            MainUser = await _userRepo.Update(MainUser.UserId, MainUser, "Users");
        }

        private async void CreateContact()
        {
            var isSuccessful = await _userRepo.Create(MainUser, "Users");

            if (isSuccessful != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success","User added to the DB", "Close");
            }
        }

        public async void TestRepo()
        { 
            AppUsers = new ObservableCollection<User>( await _userRepo.GetAll("Users"));
        }
    }
}
