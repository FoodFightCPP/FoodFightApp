using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodFight.ViewModels
{
    public class ProfileEditViewModel : BindableBase
    {
        INavigationService _navigationService;

        public ProfileEditViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
