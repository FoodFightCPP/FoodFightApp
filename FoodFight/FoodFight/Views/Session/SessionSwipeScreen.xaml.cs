using Xamarin.Forms;

using SwipeDirection = SwipeCards.Controls.Arguments.SwipeDirection;

namespace FoodFight.Views.Session
{
    public partial class SessionSwipeScreen : ContentPage
    {
        public SessionSwipeScreen()
        {
            InitializeComponent();
        }

        void CardStackView_Swiped(object sender, SwipeCards.Controls.Arguments.SwipedEventArgs e)
        {

        }

        void RestartButton_Clicked(object sender, System.EventArgs e)
        {
            CardStackView.Setup();
        }

        void SwipeLeftButton_Clicked(object sender, System.EventArgs e)
        {
            CardStackView.Swipe(SwipeDirection.Left);
        }

        void SwipeRightButton_Clicked(object sender, System.EventArgs e)
        {
            CardStackView.Swipe(SwipeDirection.Right);
        }
    }
}
