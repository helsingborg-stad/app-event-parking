using Xamarin.Forms;

namespace EventParkering.View
{
    public partial class ParkPage : ContentPage
    {
        public ParkPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            ClosestLabel.Text = Language.AppResource.ClosestLabelResx;
            ParkingSpotLabel.Text = Language.AppResource.ParkingSpotLabelResx;
            MapShowsLabel.Text = Language.AppResource.MapShowsLabelResx;

            if (Device.RuntimePlatform == Device.iOS)
            {
                buttonMargin.Margin = new Thickness(30,0,0,0);
            }
        }
    }
}
    