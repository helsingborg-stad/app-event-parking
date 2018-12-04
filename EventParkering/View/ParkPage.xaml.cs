using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace EventParkering.View
{
    public partial class ParkPage : ContentPage
    {
        public ParkPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            GetMap();

            ClosestLabel.Text = Language.AppResource.ClosestLabelResx;
            ParkingSpotLabel.Text = Language.AppResource.ParkingSpotLabelResx;
            MapShowsLabel.Text = Language.AppResource.MapShowsLabelResx;

            if (Device.RuntimePlatform == Device.iOS)
            {
                TitleLabel.Margin = new Thickness(60, 30, 30, 0);
                BackButton.Margin = new Thickness(-40, 30, 0, 0);
            }
            else
            {
                TitleLabel.Margin = new Thickness(70, 0, 30, 0);
                BackButton.Margin = new Thickness(-40, 0, 0, 0);
            }
        }

        void GetMap()
        {
            var assembly = typeof(ParkPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("EventParkering.Services.SilverJsonStyle.json");
            string Json = "";
            using (var reader = new StreamReader(stream))
            {
                Json = reader.ReadToEnd();
            }
            MyMap.MapStyle = MapStyle.FromJson(Json);
            Debug.WriteLine(Json);

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(56.044402, 12.690118), Distance.FromMeters(10000)), true);              
        }
    }
}
    