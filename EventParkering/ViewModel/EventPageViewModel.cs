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
        private EventItem _eventItem;
        private EventItem _selectedEvent;
        private List<EventItem> _itemSource;

        public IEnumerable ItemSource
        {
            get => _itemSource;
            set
            {
                if (value != null && value is IEnumerable<EventItem> items)
                    SetProperty(ref _itemSource, items.ToList());
            }
        }

        public EventItem EventtItem
        {
            get => _eventItem;
            set => SetProperty(ref _eventItem, value);
        }

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
            ItemSource = await _restService.RefreshDataAsync();
        }
    }
}
