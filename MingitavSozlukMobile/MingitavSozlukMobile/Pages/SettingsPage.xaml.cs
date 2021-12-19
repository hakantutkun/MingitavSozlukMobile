using MingitavSozlukMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MingitavSozlukMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            switch (Settings.Theme)
            {
                case 0:
                    radioSystem.IsChecked = true;
                    break;
                case 1:
                    radioLight.IsChecked = true;
                    break;
                case 2:
                    RadioDark.IsChecked = true;
                    break;
            }

            fontSizeSlider.Value = 12;
            fontSizeIndicator.Text = "12";
        }

        private void fontSizeSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            fontSizeIndicator.Text = ((Slider)sender).Value.ToString();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void RadioSystemTapped(object sender, EventArgs e)
        {
            radioSystem.IsChecked = true;
            Settings.Theme = 0;

            TheTheme.SetTheme();
        }

        private void RadioLightTapped(object sender, EventArgs e)
        {
            radioLight.IsChecked = true;
            Settings.Theme = 1;

            TheTheme.SetTheme();
        }

        private void RadioDarkTapped(object sender, EventArgs e)
        {
            RadioDark.IsChecked = true;
            Settings.Theme = 2;

            TheTheme.SetTheme();
        }

        private void radioSystem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Settings.Theme = 0;

            TheTheme.SetTheme();
        }

        private void radioLight_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Settings.Theme = 1;

            TheTheme.SetTheme();
        }

        private void RadioDark_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Settings.Theme = 2;

            TheTheme.SetTheme();
        }
    }
}