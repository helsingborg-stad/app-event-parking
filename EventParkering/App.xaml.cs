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

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace EventParkering
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            AppCenter.Start("android=4e16bab9-8bcc-4b13-8801-20f4b8868aab;" +
                            "ios=99fb363d-2ed3-4bd5-bc53-c75d88915624",
                  typeof(Analytics), typeof(Crashes));

            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
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
    }
}
