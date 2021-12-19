using MingitavSozlukMobile.Services;
using Plugin.Share;
using Plugin.Share.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MingitavSozlukMobile.Pages
{
    /// <summary>
    /// The page that shows all details of the selected word.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        /// <summary>
        /// The word instance for selected word.
        /// </summary>
        private Kelime _kelime;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="kelime">Selected word that comes from Main Page</param>
        /// <param name="ornekList">The list of examples of the word</param>
        public DetailPage(Kelime kelime, List<Ornek> ornekList)
        {
            // Initialize components
            InitializeComponent();

            // Inject the selected word for globally usage.
            _kelime = kelime;

            // Set the text of word label.
            wordLabel.Text = kelime.KaracaycaAnlam;

            // Set the text of meaning label.
            meaningLabel.Text = kelime.TurkceAnlam;

            // Set the item source of the Examples list view.
            ornekListView.ItemsSource = ornekList;

            // Check if the word saved as favourite before.
            if(kelime.IsFav.Equals(1))
            {
                // Hide save button
                saveButton.IsVisible = false;
                
                // Show Remove button
                removeButton.IsVisible = true;
            }

            // Update the word asynchronously.
            Task.Run(() =>
            {
                // Set the word's searched tag to true.
                kelime.IsSearched = 1;

                // Set the word's search time to current time.
                kelime.SearchTime = DateTime.Now.ToString();

                // Update the word
                DBService.UpdateWord(kelime).Wait();

            }).Wait();
        }

        /// <summary>
        /// Fires off when save button clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            // Check if the word saved before
            if(_kelime.IsFav.Equals(1))
            {
                // If saved, remove word
                _kelime.IsFav = 0;

                // Update removed word
                await DBService.UpdateWord(_kelime);

                // Display an alert to user which indicates that word has been removed
                await DisplayAlert("Bilgi", "Kelime kaydedilen kelimlelerden çıkarıldı.", "Tamam");

                // Show save button back.
                saveButton.IsVisible = true;
                    
                // Remove hide button.
                removeButton.IsVisible = false;
            }
            // if word has not been saved before,
            else
            {
                // Set the word as saved
                _kelime.IsFav = 1;

                // Set the wword's favourite time to current time
                _kelime.FavTime = DateTime.Now.ToString();

                // Update the word as saved.
                await DBService.UpdateWord(_kelime);

                // Display an alert to user which indicates that word has been added to favourites.
                await DisplayAlert("Bilgi", "Kelime kaydedilen kelimlelere eklendi..", "Tamam");

                // Hide save button.
                saveButton.IsVisible = false;
                
                // Show remove button.
                removeButton.IsVisible = true;
            }
        }

        /// <summary>
        /// Fires when copy button clicked. Copies the word to the clipboard. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void copyButton_Clicked(object sender, EventArgs e)
        {
            // Copy the word to the clipboard.
            await Clipboard.SetTextAsync(_kelime.KaracaycaAnlam);

            // Display an alert to the user which indicates that word has been copied to the clipboard.
            await DisplayAlert("Bilgi", "Kelime panoya başarıyla kopyalandı.", "Tamam");
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
            await DisplayAlert("Bilgi", "Bu özellik henüz kullanılamıyor.", "Tamam");
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
                Text = _kelime.KaracaycaAnlam + " : " + _kelime.TurkceAnlam
            }) ;
        }

        /// <summary>
        /// Closes the detail page. Goes back to the previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}