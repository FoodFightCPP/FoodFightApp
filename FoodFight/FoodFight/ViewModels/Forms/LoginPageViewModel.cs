﻿using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using CryptoService;
using FoodFight.Domain.Models;
using FoodFight.Domain.Services;

namespace FoodFight.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : LoginViewModel
    {
        #region Fields

        private string password;
        readonly INavigationService _navigationService;
        readonly IDataService<User> _userRepo;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel(INavigationService navigation, IDataService<User> userRepo)
        {
            _navigationService = navigation;
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
            _userRepo = userRepo;
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>
        public Command LoginCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            try
            {
                User mainUser = await _userRepo.GetByEmail(Email.ToLower(), "Users");
                if (Crypto.IsValidPassword(Password, mainUser.Salt, mainUser.Password))
                {
                    var user = new NavigationParameters
                    {
                        { "MainUser", mainUser }
                    };
                    await _navigationService.NavigateAsync("/MainPage?selectedTab=Start", user);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Incorrect Email or Password, Please try again!", "Close");
                }
            }
            catch (System.Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Account Not Found", "Sorry that account does not exist! Please sign up for a free account", "Close");
                Email = "";
                Password = "";
            }
            
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            await _navigationService.NavigateAsync("SimpleSignUpPage");
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;

            await _navigationService.NavigateAsync("SimpleForgotPasswordPage");
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SocialLoggedIn(object obj)
        {
            await _navigationService.NavigateAsync("/MainPage?selectedTab=Home");
        }

        #endregion
    }
}