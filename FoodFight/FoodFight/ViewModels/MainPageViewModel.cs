using FoodFight.Domain.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FoodFight.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigatedAware
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            
        }
    }
}
