using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EventParkering.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            FindLabel.Text = Language.AppResource.FindLabelResx;
            ParkLabel.Text = Language.AppResource.ParkLabelResx;
            VersionLabel.Text = Language.AppResource.VersionLabelResx;
            InfoLabel.Text = Language.AppResource.InfoLabelResx;
            TryLabel.Text = Language.AppResource.TryLabelResx;
            KnowMoreLabel.Text = Language.AppResource.KnowMoreLabelResx;

            if (Device.RuntimePlatform == Device.iOS)
            {
                StackPadding.Padding = new Thickness(0, 190, 0, 0);
            }
            else
            {
                StackPadding.Padding = new Thickness(0, 170, 0, 0);
            }
        }
    }
}
