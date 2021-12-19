using Xamarin.Forms;

namespace MingitavSozlukMobile.Helpers
{
    public static class TheTheme
    {
        public static void SetTheme()
        {
            switch (Settings.Theme)
            {
                // default
                case 0:
                    Application.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                // light
                case 1:
                    Application.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                // dark
                case 2:
                    Application.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var e = DependencyService.Get<IEnvironment>();
            if(App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(new Color(0, 0, 0), false);
            }
            else
            {
                e?.SetStatusBarColor(new Color(248, 248, 248, 0), true);
            }
        }
    }
}
