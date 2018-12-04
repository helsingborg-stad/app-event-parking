using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using EventParkering.Model;
using EventParkering.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace EventParkering.ViewModel
{
    public class ParkPageViewModel : BaseViewModel
    {
        public ObservableCollection<Pin> Pins { get; set; }

        public Command<MapClickedEventArgs> MapClickedCommand =>
            new Command<MapClickedEventArgs>(args =>
            {
                Pins.Add(new Pin
                {
                    /*Label = $"Pin{Pins.Count}",
                    Position = args.Point*/
                });
            });

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

        private EventItem _selectedString;
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

        public string ParkName { get; set; }
        public string ParkLat { get; set; }
        public string ParkLon { get; set; }
        public string ParkDist { get; set; }

        public EventItem SelectedString
        {
            get { return _selectedString; }
            set { SetProperty(ref _selectedString, value); }
        }

        public DelegateCommand GoBack { get; set; }

        public ParkPageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            GoBack = new DelegateCommand(() =>
            {
                _navigationService.GoBackAsync();
            });

            //RestService.ParkDataAsync();
        }

        public async Task<bool> GetParkingSpot()
        {
            try
            {
               
            }
            catch (Exception err)
            {
                Debug.WriteLine("Kunde inte hämta pins {0}", err);
            }
            return true;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            EventItem = (EventItem)parameters["Event"];
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }
    }
}
