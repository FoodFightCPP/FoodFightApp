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

        ObservableCollection<User> _users;

        User _user;

        public User MainUser 
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public ObservableCollection<User> AppUsers { 
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public ContactsViewModel(IDataService<User> userRepo)
        {
            _userRepo = userRepo;
            MainUser = new User()
            {
                UserId = new Guid("67A03584-68E0-4E04-8214-D969C66F308A"),
                Name = "Pete BumMuffin",
                Bio = "This is a bio, I know real original",
                Street = "1234 Nowhere Lane",
                City = "FairyLand",
                State = "Michigan",
                Email = "123DontCareAboutMe@whatever.com",
                Gender = "Male",
                Password = "123456789",
                Phone = "231-555-5555",
                Username = "p#12345",
                Facebook = "",
                Instagram = "",
                Twitter = "",
                ZipCode = "49690",
                Website = "",
                ProfilePicture = ""
            };
            TestRepo();
            //CreateContact();
            //UpdateContact();
            //DeleteUser();

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
