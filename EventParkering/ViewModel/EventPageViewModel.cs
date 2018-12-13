using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventParkering.Model;
using EventParkering.Services;
using Prism.Navigation;

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
        }

        public async override void OnNavigatingTo(INavigationParameters parameters)
        {
            EventList = await _restService.EventDataAsync();
        }
    }
}
