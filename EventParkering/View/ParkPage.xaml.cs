using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using EventParkering.Services;
using Plugin.Geolocator;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace EventParkering.View
{
    public partial class ParkPage : ContentPage
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        INavigationService navigationService;
        ParkService ParkService;
        private ViewModel.ParkPageViewModel parkViewModel;

        public ParkPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            //GetMap();

            ClosestLabel.Text = Language.AppResource.ClosestLabelResx;
            ParkingSpotLabel.Text = Language.AppResource.ParkingSpotLabelResx;
            MapShowsLabel.Text = Language.AppResource.MapShowsLabelResx;

            /*if (Device.RuntimePlatform == Device.iOS)
            {
                TitleLabel.Margin = new Thickness(60, 30, 30, 0);
                BackButton.Margin = new Thickness(-40, 30, 0, 0);
            }
            else
            {
                TitleLabel.Margin = new Thickness(70, 0, 30, 0);
                BackButton.Margin = new Thickness(-40, 0, 0, 0);
            }*/
        }

        async void GetMap()
        {
            //parkViewModel = new ViewModel.ParkPageViewModel(navigationService, ParkService);

            /*var assembly = typeof(ParkPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("EventParkering.Services.SilverJsonStyle.json");
            string Json = "";
            using (var reader = new StreamReader(stream))
            {
                Json = reader.ReadToEnd();
            }
            MyMap.MapStyle = MapStyle.FromJson(Json);*/

            // await GetCurrentLocation();

            //await parkViewModel.GetParkingSpot();

        }

        private async Task<bool> GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;
            if (!CrossGeolocator.IsSupported)
                return false;
            if (!CrossGeolocator.Current.IsGeolocationAvailable)
                return false;
            try
            {
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                latitude = position.Latitude;
                longitude = position.Longitude;
               // MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(56.043980, 12.688751), Distance.FromMeters(10000)), true);

                return true;
            }
            catch (Exception e)
            {
                string reason = e.Message;
                return false;
            }
        }
    }
}
    