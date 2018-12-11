using System;
using System.Globalization;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace EventParkering.View
{
    public partial class EventPage : ContentPage
    {
        public EventPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            CultureInfo MyCultureInfo = new CultureInfo("se-SE");
            string Month = DateTime.Now.ToString("dd-MMMM");
            DateTime MyDateTime = DateTime.Parse(Month, MyCultureInfo);
            TodaysDate.Text = "IDAG " + MyDateTime.ToString("dd MMMM").ToUpper();

            HelloLabel.Text = Language.AppResource.HelloLabelResx;
            WhereGoLabel.Text = Language.AppResource.WhereGoLabelResx;
            Drivelabel.Text = Language.AppResource.DrivelabelResx;
            ChooseLabel.Text = Language.AppResource.ChooseLabelResx;

            /*if (Device.RuntimePlatform == Device.iOS)
            {
                WaveGif = new CachedImage { Source = "resource://EventParkering.waving.gif" };
            }
            else
            {
                WaveGif = new CachedImage { Source = "resource://EventParkering.waving.gif" };
            }*/

            listView.ItemSelected += (sender, args) => listView.SelectedItem = null;
        }
    }
}
    