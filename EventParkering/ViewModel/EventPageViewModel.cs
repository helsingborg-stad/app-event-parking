using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EventParkering.Model;
using EventParkering.Services;
using Prism.Navigation;
using Xamarin.Forms;

namespace EventParkering.ViewModel
{
    public class EventPageViewModel : BaseViewModel
    {
        private readonly IRestService _restService;
        private EventItem _eventItem;
        private EventItem _selectedEvent;
        private List<EventItem> _itemSource;

        //Item Source Binding Property
        public IEnumerable ItemSource
        {
            get => _itemSource;
            set
            {
                if (value != null && value is IEnumerable<EventItem> items)
                    SetProperty(ref _itemSource, items.ToList());
            }
        }

        // ReSharper disable once IdentifierTypo
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
                //Move to another page if selected is not null
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

        /*private EventItem _selectedEvent { get; set; }
        public EventItem SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                if (_selectedEvent != value)
                {
                    _selectedEvent = value;
                    var parameter = new NavigationParameters {  { "TitleValue", _selectedEvent?.name} };

                    Debug.WriteLine(_selectedEvent.name + _selectedEvent.streetAddress);

                    _navigationService.NavigateAsync("ParkPage", parameter);
                }
            }
        }*/

        private async void LoadEventItems()
        {
            ItemSource = await _restService.RefreshDataAsync();
        }
    }
}
