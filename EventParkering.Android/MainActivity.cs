using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
using Prism;
using Prism.Ioc;
using EventParkering.ViewModel;
using Xamarin.Forms;
using EventParkering.Services;
using Plugin.Permissions;
using Plugin.CurrentActivity;
using Plugin.Permissions.Abstractions;
using Prism.Services;

namespace EventParkering.Droid
{
    [Activity(Label = "EventParkering", Icon = "@mipmap/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IPageDialogService _pageDialogService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init();

            Xamarin.FormsMaps.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnResume()
        {
            base.OnResume();

            RequestPermissions(_pageDialogService);
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                containerRegistry.RegisterForNavigation<NavigationPage>();
                containerRegistry.RegisterForNavigation<View.MainPage, MainPageViewModel>();
                containerRegistry.RegisterForNavigation<View.EventPage, EventPageViewModel>();
                containerRegistry.RegisterForNavigation<View.ParkPage, ParkPageViewModel>();
                containerRegistry.Register<IRestService, RestService>();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void RequestPermissions(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Location))
                    {
                        await _pageDialogService.DisplayAlertAsync("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Location))
                        status = results[Plugin.Permissions.Abstractions.Permission.Location];
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await _pageDialogService.DisplayAlertAsync("Location Denied", "Cannot continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                //Exception ex;
                //LabelGeolocation.Text = "Error: " + ex;
            }
        }
    }
}