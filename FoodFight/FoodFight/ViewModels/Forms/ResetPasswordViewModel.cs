﻿using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FoodFight.ViewModels.Forms
{
    /// <summary>
    /// ViewModel for reset password page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ResetPasswordViewModel : BaseViewModel
    {
        #region Fields

        private string newPassword;

        private string confirmPassword;

        readonly INavigationService _navigationService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordViewModel" /> class.
        /// </summary>
        public ResetPasswordViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            this.SubmitCommand = new Command(this.SubmitClicked);
            this.SignUpCommand = new Command(this.SignUpClicked);
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Submit button is clicked.
        /// </summary>
        public Command SubmitCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        #endregion

        #region Public property

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password from user in the reset password page.
        /// </summary>
        public string NewPassword
        {
            get
            {
                return this.newPassword;
            }

            set
            {
                if (this.newPassword == value)
                {
                    return;
                }

                this.newPassword = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that bounds with an entry that gets the new password confirmation from the user in the reset password page.
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

        #region Methods

        /// <summary>
        /// Invoked when the Submit button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SubmitClicked(object obj)
        {
            await _navigationService.NavigateAsync("SimpleLoginPage");
        }

        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SignUpClicked(object obj)
        {
            await _navigationService.NavigateAsync("SimpleSignUpPage");
        }

        #endregion
    }
}