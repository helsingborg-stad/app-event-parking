using Xamarin.Forms;

namespace EventParkering.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            FindLabel.Text = Language.AppResource.FindLabelResx;
            ParkLabel.Text = Language.AppResource.ParkLabelResx;
            VersionLabel.Text = Language.AppResource.VersionLabelResx;
            InfoLabel.Text = Language.AppResource.InfoLabelResx;
            TryLabel.Text = Language.AppResource.TryLabelResx;
            KnowMoreLabel.Text = Language.AppResource.KnowMoreLabelResx;

        }
    }
}
