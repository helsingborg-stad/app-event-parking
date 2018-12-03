using System;
using EventParkering.Model;
using Prism.Commands;
using Prism.Navigation;

namespace EventParkering.ViewModel
{
    public class ParkPageViewModel : BaseViewModel
    {

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

        private EventItem _selectedString;
        private EventItem _eventItem;

        public EventItem EventItem
        {
            get => _eventItem;
            set
            {
                _eventItem = value;
                Title = value.name;
                Address = value.streetAddress;
            }
        }

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
