using Xamarin.Essentials;

namespace MingitavSozlukMobile.Helpers
{
    public static class Settings
    {
        // 0 = default, 1 = light, 2 = dark
        const int theme = 0;

        // Save number of words to preferences once program started...
        const int numberOfWords = 0;

        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }

        public static int NumberOfWords
        {
            get => Preferences.Get(nameof(NumberOfWords), numberOfWords);
            set => Preferences.Set(nameof(NumberOfWords), value);
        }

    }
}
