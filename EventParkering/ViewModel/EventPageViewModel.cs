using EventParkering.Model;
using Prism.Navigation;
using Xamarin.Forms;

namespace EventParkering.ViewModel
{
    public class EventPageViewModel
    {
        private readonly INavigationService _navigationService;      

        public EventPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private EventItem _selectedEvent { get; set; }
        public EventItem SelectedEvent
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
}
