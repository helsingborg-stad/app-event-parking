using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EventParkering.View
{
    public partial class ParkPage : ContentPage
    {
        public ParkPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            ClosestLabel.Text = Language.AppResource.ClosestLabelResx;
            ParkingSpotLabel.Text = Language.AppResource.ParkingSpotLabelResx;
            MapShowsLabel.Text = Language.AppResource.MapShowsLabelResx;

            if (Device.RuntimePlatform == Device.iOS)
            {
                TitleLabel.Margin = new Thickness(70, 30, 30, 0);
                BackButton.Margin = new Thickness(-40, 30, 0, 0);
            }
            else
            {
                TitleLabel.Margin = new Thickness(70, 0, 30, 0);
                BackButton.Margin = new Thickness(-40, 0, 0, 0);
            }
        }
    }
}
