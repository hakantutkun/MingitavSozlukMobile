using MingitavSozlukMobile.Helpers;
using MingitavSozlukMobile.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MingitavSozlukMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            TheTheme.SetTheme();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestThemeChanged;
        }

        private void App_RequestThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                OSAppTheme currentTheme = Current.RequestedTheme;

                TheTheme.SetTheme();
            });
        }
    }
}
