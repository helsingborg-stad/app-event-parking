using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EventParkering.Model;
using EventParkering.Services;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace EventParkering.ViewModel
{
    public class EventPageViewModel : BaseViewModel
    {
        private readonly IRestService _restService;

        private List<EventItem> eventList;
        public IEnumerable EventList
        {
            get => eventList;
            set
            {
                if (value != null && value is IEnumerable<EventItem> items)
                    SetProperty(ref eventList, items.ToList());
            }
        }

        private EventItem _eventItem;
        public EventItem EventtItem
        {
            get => _eventItem;
            set => SetProperty(ref _eventItem, value);
        }

        private EventItem _selectedEvent;
        public EventItem SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                if (value != null)
                {
                    var parameter = new NavigationParameters { { "Event", value } };
                    _navigationService.NavigateAsync("ParkPage", parameter);
                }
                SetProperty(ref _selectedEvent, value);
            }
        }

        public EventPageViewModel(INavigationService navigationService, IRestService restService)
        : base(navigationService)
        {
            _restService = restService;
            LoadEventItems();
        }

        private async void LoadEventItems()
        {
            EventList = await _restService.EventDataAsync();
            /*var e = await _restService.ParkDataAsync(581381, "1000");

            try
            {
                // så?
                foreach (var i in e)
                {
                    var parklat = Convert.ToDouble(i.lat, System.Globalization.CultureInfo.InvariantCulture);
                    var parklon = Convert.ToDouble(i.lat, System.Globalization.CultureInfo.InvariantCulture);

                    //var eventlat = Convert.ToDouble(Lat, System.Globalization.CultureInfo.InvariantCulture);
                    //var eventlon = Convert.ToDouble(Lon, System.Globalization.CultureInfo.InvariantCulture);

                    var parkPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(parklat, parklon),
                        Label = i.name
                    };

                    var eventPin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(eventlat, eventlon),
                        Label = Title
                    };
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("Kunde inte hämta pins {0}", err);
            }*/


        }
    }
}
