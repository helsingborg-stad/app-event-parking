using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EventParkering.Model;
using EventParkering.Services;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace EventParkering.ViewModel
{
    public class ParkPageViewModel : BaseViewModel
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Xamarin.Forms.GoogleMaps.Map Map { get; private set; }

        ///private readonly IRestService _restService;
        ParkService _parkService;


        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _id;
        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _lat;
        public string Lat
        {
            get { return _lat; }
            set { SetProperty(ref _lat, value); }
        }

        private string _lon;
        public string Lon
        {
            get { return _lon; }
            set { SetProperty(ref _lon, value); }
        }

        private EventItem _eventItem;
        public EventItem EventItem
        {
            get => _eventItem;
            set
            {
                _eventItem = value;
                Title = value.name;
                var address = value.streetAddress;
                Address = address + ", Helsingborg";
                ID = value.id;
                Lat = value.lat;
                Lon = value.lon;
            }
        }

        public DelegateCommand GoBack { get; set; }

        public ParkPageViewModel(INavigationService navigationService, ParkService parkService)
        : base(navigationService)
        {
            //_restService = restService;
            _parkService = parkService;
            GoBack = new DelegateCommand(() =>
            {
                _navigationService.GoBackAsync();
            });

            Map = new Xamarin.Forms.GoogleMaps.Map();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(56.043980, 12.688751), Xamarin.Forms.GoogleMaps.Distance.FromMeters(1000)));
            Map.MyLocationEnabled = true;


            //Map.InitialCameraUpdate(55.608548, 12.992234, 14, 30, 60);
            //Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(18.48, -69.93), Xamarin.Forms.GoogleMaps.Distance.FromKilometers(40)));

            //callParkingSpot.Execute(true);
        }

        //Command callParkingSpot => new Command(async () => { await GetParkingSpot(); });


        public async Task GetParkingSpot()
        {
            GetMapStyle();
            await GetCurrentLocation();
            var parkDataAsync = await _parkService.ParkDataAsync(581381, "1000");
            //return;

            try
            {

                foreach (var i in parkDataAsync)
                {
                    var parklat = Convert.ToDouble(i.lat, System.Globalization.CultureInfo.InvariantCulture);
                    var parklon = Convert.ToDouble(i.lat, System.Globalization.CultureInfo.InvariantCulture);

                    var eventlat = Convert.ToDouble(Lat, System.Globalization.CultureInfo.InvariantCulture);
                    var eventlon = Convert.ToDouble(Lon, System.Globalization.CultureInfo.InvariantCulture);

                    var parkPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(56.043980, 12.688751),
                        Label = "hej"
                    };

                    var eventPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(56.043980, 12.688751),
                        Label = "fin"
                    };
                    Map.Pins.Add(parkPin);
                    Map.Pins.Add(eventPin);
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("Kunde inte hämta pins {0}", err);
            }
        }

        public void GetMapStyle()
        {
            var assembly = typeof(ParkPageViewModel).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("EventParkering.Services.SilverJsonStyle.json");
            string Json = "";
            using (var reader = new StreamReader(stream))
            {
                Json = reader.ReadToEnd();
            }
            Map.MapStyle = MapStyle.FromJson(Json);
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
                //Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(latitude, longitude), Distance.FromMeters(10000)), true);

                return true;
            }
            catch (Exception e)
            {
                string reason = e.Message;
                return false;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            EventItem = (EventItem)parameters["Event"];           
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            await GetParkingSpot();
        }
    }
}
