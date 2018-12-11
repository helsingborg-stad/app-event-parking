using System;
using EventParkering.Services;
using EventParkering.ViewModel;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using System.Diagnostics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EventParkering
{
    public partial class App : PrismApplication
    {
        IPageDialogService _pageDialogService;

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            AppCenter.Start("android=4e16bab9-8bcc-4b13-8801-20f4b8868aab;" +
                            "ios=99fb363d-2ed3-4bd5-bc53-c75d88915624",
                  typeof(Analytics), typeof(Crashes));

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                RequestPermissions(_pageDialogService);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRestService, RestService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<View.MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<View.EventPage, EventPageViewModel>();
            containerRegistry.RegisterForNavigation<View.ParkPage, ParkPageViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private async void RequestPermissions(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location);

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync();
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await _pageDialogService.DisplayAlertAsync("Location Denied", "Cannot continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: {0}", ex);
            }
        }
    }
}
