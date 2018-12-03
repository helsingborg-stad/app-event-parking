using System;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace EventParkering.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        IPageDialogService _pageDialogService;
        public DelegateCommand FindEvents { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
         : base(navigationService)
        {

            _pageDialogService = pageDialogService;
            FindEvents = new DelegateCommand(() =>
            {
                _navigationService.NavigateAsync("EventPage");               
            });

        }
       
    }   
}
