using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace FoodFight.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        readonly IDataService<User> _dataService;
        ObservableCollection<User> _appUser;
        User _mainUser;

        public User MainUser 
        {
            get => _mainUser;
            set => SetProperty(ref _mainUser, value);
        }

        public HomeViewModel(IDataService<User> dataService)
        {
            _dataService = dataService;
            GetName();
        }

        private async void GetName()
        {
            
            MainUser = await _dataService.Get(6, "Users");
        }
    }
}
