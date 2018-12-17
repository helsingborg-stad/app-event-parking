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

            listView.ItemSelected += (sender, args) => listView.SelectedItem = null;
            listView.HeightRequest = listView.RowHeight * 10;

            if (Device.RuntimePlatform == Device.Android)
            {
                WaveGif.WidthRequest = 60;
            }
        }
    }
}
    