using MingitavSozlukMobile.Pages;
using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MingitavSozlukMobile.Views
{
    /// <summary>
    /// A view that contains the words that user saved as favourite.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritesView : ContentView
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public FavouritesView()
        {
            // Initialize the components
            InitializeComponent();

            // Set the binding context
            BindingContext = FavouritesViewModel.Instance;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes the selected word from the favourites list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButton_Clicked(object sender, System.EventArgs e)
        {
            // Get the clicked item as button.
            Button btn = sender as Button;

            // Get the selected word by using the button's binding context
            Kelime kelime = (Kelime)btn.BindingContext;

            // Remove word from the list by using view model.
            ((FavouritesViewModel)BindingContext).RemoveWord(kelime);
        }

        /// <summary>
        /// Redirects to detail page when an item of the list tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void wordListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the list view item as tapped item.
            ListView item = sender as ListView;

            // Get the selected word by casting the selected item.
            var kelime = (Kelime)item.SelectedItem;

            // Get the example list for sending to detail page.
            List<Ornek> ornekList = await DBService.LoadExamples(kelime.KelimeID);

            // Redirect to the detail page with selected word and examples.
            await Navigation.PushModalAsync(new DetailPage(kelime, ornekList));
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                if (Application.Current.UserAppTheme == OSAppTheme.Dark)
                {
                    viewCell.View.BackgroundColor = Color.DarkGray;
                }
                else
                {
                    viewCell.View.BackgroundColor = Color.LightGray;
                }
            }
        }

        #endregion
    }
}