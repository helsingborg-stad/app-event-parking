using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using EventParkering.Model;
using EventParkering.Services;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;
using Plugin.ExternalMaps;

namespace EventParkering.ViewModel
{
    public class ParkPageViewModel : BaseViewModel
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public Xamarin.Forms.GoogleMaps.Map Map { get; private set; }

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
        public EventItem eventItem
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
            _parkService = parkService;
            GoBack = new DelegateCommand(() =>
            {
                _navigationService.GoBackAsync();
            });

            Map = new Xamarin.Forms.GoogleMaps.Map();

            Map.InitialCameraUpdate = CameraUpdateFactory.NewCameraPosition(new CameraPosition(new Position(56.04673, 12.69437), 15d));

            Map.MyLocationEnabled = true;
        }

        public async Task GetParkingSpot()
        {
            GetMapStyle();
            await GetCurrentLocation();
            var parkDataAsync = await _parkService.ParkDataAsync(eventItem.id, "99999999999");

            try
            {
                foreach (var i in parkDataAsync)
                {
                    BitmapDescriptor SetPinIconStream(string embeddedResource)
                    {
                        var assembly = typeof(ParkPageViewModel).GetTypeInfo().Assembly;
                        var stream = assembly.GetManifestResourceStream($"" + embeddedResource);
                        return BitmapDescriptorFactory.FromStream(stream);
                    }

                    var eventlat = Convert.ToDouble(Lat, System.Globalization.CultureInfo.InvariantCulture);
                    var eventlon = Convert.ToDouble(Lon, System.Globalization.CultureInfo.InvariantCulture);

                    Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(eventlat, eventlon), Distance.FromMeters(250)));

                    var parkPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(i.lat, i.lon),
                        Label = i.name + " - " + i.dist + " meter från eventet.",
                        Icon = SetPinIconStream("EventParkering.parkingSpot.png"),
                    };

                    var eventPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(eventlat, eventlon),
                        Label = Title,
                    };
                    Map.Pins.Add(parkPin);
                    Map.Pins.Add(eventPin);

                    parkPin.Clicked += (sender, args) =>
                    {
                        CrossExternalMaps.Current.NavigateTo(i.name, i.lat, i.lon);
                    };
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("Error: {0}", err);
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
          
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            eventItem = (EventItem)parameters["Event"];
            await GetParkingSpot();
        }      
    }
}
