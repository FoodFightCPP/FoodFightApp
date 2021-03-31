using FoodFight.Domain.Models;
using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using CryptoService;
using FoodFight.Domain.Services;
using System.Linq;
using System.Text.RegularExpressions;

namespace FoodFight.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for sign-up page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SignUpPageViewModel : LoginViewModel
    {
        #region Fields

        private string name;

        private string password;

        private string confirmPassword;

        string userName;

        readonly INavigationService _navigationService;

        IDataService<User> _mainUser;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="SignUpPageViewModel" /> class.
        /// </summary>
        public SignUpPageViewModel(INavigationService navigationService, IDataService<User> mainUser)
        {
            this.LoginCommand = new Command(this.LoginClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
            _navigationService = navigationService;
            _mainUser = mainUser;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the name from user in the Sign Up page.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name == value)
                {
                    return;
                }

                this.name = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password from users in the Sign Up page.
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

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the password confirmation from users in the Sign Up page.
        /// </summary>
        public string ConfirmPassword
        {
            get
            {
                return this.confirmPassword;
            }

            set
            {
                if (this.confirmPassword == value)
                {
                    return;
                }

                this.confirmPassword = value;
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

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Log in button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void LoginClicked(object obj)
        {
            await _navigationService.NavigateAsync("SimpleLoginPage");
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            // Code for sending user data to the database and registering the user
            // Logic to create unique Username for connecting Users i.e. firstinital#12345 (p#12345)

            bool validName = false;
            bool validEmail = false;
            while (!validName)
            {
                if (name == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You must enter Name i.e. John Jones", "Close");
                    break;
                }
                else if (name == "")
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You must enter a First and Last name only! It cannot be blank!", "Close");
                    break;
                }
                else if (!Regex.Match(name, @"^[a-z A-Z]*$").Success)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Name can only have letters!", "Close");
                    break;
                }
                else
                {
                    try
                    {
                        var splitName = name.Split(' ');
                        var firstName = char.ToUpper(splitName[0][0]) + splitName[0].Substring(1);
                        var lastName = char.ToUpper(splitName[1][0]) + splitName[1].Substring(1);
                        userName = firstName + lastName;
                        validName = true;
                    }
                    catch (Exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "You must enter a First and Last name separated by a space!", "Close");
                        name = "";
                        break;
                    }
                }
            }

            if (Password == null || Password == "" || ConfirmPassword == null || ConfirmPassword == "")
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please make sure you have filled in the password fields!", "Close");
            }

            if (Email == null || Email == "")
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please make sure you have filled in the Email Field!", "Close");
            }
            else
            {
                var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                if (!regex.IsMatch(Email) && !Email.EndsWith("."))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Email is not valid. Please try again!", "Close");
                } else
                {
                    validEmail = true;
                }
            }


            if (validName && validEmail && Password != "" && Password != null && ConfirmPassword != "" && ConfirmPassword != null)
            {

                if (password.Equals(confirmPassword))
                {
                    var salt = Crypto.GenerateSalt();
                    var hashPass = Crypto.ComputeHash(password, salt);

                    var rand = new Random();

                    User mainUser = new User()
                    {
                        UserId = Guid.NewGuid(),
                        Name = name.Trim().ToLower(),
                        Email = Email.ToLower(),
                        Password = Convert.ToBase64String(hashPass),
                        Salt = Convert.ToBase64String(salt),
                        Username = userName + "#" + rand.Next(0, 1000000).ToString("D6")
                    };

                    await _mainUser.Create(mainUser, "Users");

                    await _navigationService.NavigateAsync("SimpleLoginPage");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Your Passwords do not match!", "Close");
                    Password = "";
                    ConfirmPassword = "";
                }
            }
            #endregion
        }
    }
}