using System;
using Prism.Commands;
using Prism.Navigation;

namespace EventParkering.ViewModel
{
    public class MainPageViewModel
    {
        private readonly INavigationService _navigationService;
        public DelegateCommand FindEvents { get; set; }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            FindEvents = new DelegateCommand(() =>
            {
                _navigationService.NavigateAsync("EventPage");               
            });
        }
    }
}
