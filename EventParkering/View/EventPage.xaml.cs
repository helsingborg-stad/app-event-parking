using System;
using System.Globalization;
using EventParkering.Services;
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

            if (Device.RuntimePlatform == Device.iOS)
            {
                WaveGif.Margin = new Thickness(-130, 45, 0, 0);
                CommaLabel.Margin = new Thickness(-100, 50, 0, 0);  
            }
            else
            {
                WaveGif.Margin = new Thickness(-180, 55, 0, 0);
                CommaLabel.Margin = new Thickness(-135, 50, 0, 0);
            }
            listView.ItemSelected += (sender, args) => listView.SelectedItem = null;
        }
    }
}
    