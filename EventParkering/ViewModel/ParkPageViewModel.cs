using System;
using Prism.Commands;
using Prism.Navigation;

namespace EventParkering.ViewModel
{
    public class ParkPageViewModel
    {
        private readonly INavigationService _navigationService;
        public DelegateCommand GoBack { get; set; }

        public ParkPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBack = new DelegateCommand(() =>
            {
                _navigationService.NavigateAsync("EventPage");
            });
        }
    }
}
