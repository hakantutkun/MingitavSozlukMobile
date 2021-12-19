using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels.Controls;
using MingitavSozlukMobile.ViewModels.Pages;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MingitavSozlukMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneWordControl : ContentView
    {

        public OneWordControl()
        {
            InitializeComponent();

            // Set the binding context
            BindingContext = OneWordControlViewModel.Instance;
        }

        /// <summary>
        /// Fires when copy button clicked. Copies the word to the clipboard. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void copyButton_Clicked(object sender, EventArgs e)
        {
            // Copy the word to the clipboard.
            await Clipboard.SetTextAsync(((OneWordControlViewModel)BindingContext).SelectedWord.KaracaycaAnlam);

            // Display an alert to the user which indicates that word has been copied to the clipboard.
            //await DisplayAlert("Bilgi", "Kelime panoya başarıyla kopyalandı.", "Tamam");
            MainPageViewModel.Instance.DisplayPopup("Bilgi", "Kelime panoya başarıyla kopyalandı.");
        }

        /// <summary>
        /// Fires when listen button clicked. Plays the sound of the word.
        /// (Not Active Yet!!!)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void listenButton_Clicked(object sender, EventArgs e)
        {
            // Display an alert to the user which indicates that this button is not active yet.
            //await DisplayAlert("Bilgi", "Bu özellik henüz kullanılamıyor.", "Tamam");
            MainPageViewModel.Instance.DisplayPopup("Bilgi", "Bu özellik henüz kullanılamıyor.");
        }

        /// <summary>
        /// Fires when share button clicked. 
        /// Creates a text that contains both meanings and shares it to desired application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void shareButton_Clicked(object sender, EventArgs e)
        {
            // Fire the share window of the app
            await CrossShare.Current.Share(new ShareMessage
            {
                // Set Title
                Title = "Mingi Tav Sözlük",

                // Set content that will be shared
                Text = ((OneWordControlViewModel)BindingContext).SelectedWord.KaracaycaAnlam + " : " + ((OneWordControlViewModel)BindingContext).SelectedWord.TurkceAnlam
            });
        }

    }
}