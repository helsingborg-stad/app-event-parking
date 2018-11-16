using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace EventParkering.ViewModel
{
    public class EventPageViewModel 
    {
        public ObservableCollection<Event> EventList { get; set; }

        private readonly INavigationService _navigationService;

        public EventPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            EventList = new ObservableCollection<Event>
            {
                new Event(){Test = "HIC at THINK Open Space: Drop-in Sessions!", TestImage="chevronright.png"},
                new Event(){Test = "Visionsfonden Open Sessions at THINK Open Space", TestImage="chevronright.png"},
                new Event(){Test = "Café Välkommen", TestImage="chevronright.png"},
                new Event(){Test = "Stickcafé", TestImage="chevronright.png"},
                new Event(){Test = "Sittgymnastik varje tisdag", TestImage="chevronright.png"},
                new Event(){Test = "Sittgymnastik varje tisdag", TestImage="chevronright.png"}
            };           
        }

        private Event _selectedEvent { get; set; }
        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                if (_selectedEvent != value)
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        _selectedEvent = null;
                    }
                    else
                    {
                        _selectedEvent = value;
                    }                 

                    _navigationService.NavigateAsync("ParkPage");
                }
            }
        }
    }

    public class Event
    {
        public string Test { get; set; }
        public string TestImage { get; set; }
    }
}
