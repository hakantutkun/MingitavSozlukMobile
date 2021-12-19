using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MingitavSozlukMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllWordsPage : ContentPage
    {
        private ViewCell _selectedCell;

        public AllWordsPage()
        {
            InitializeComponent();

            // Set the binding context
            BindingContext = AllWordsViewModel.Instance;
        }

        /// <summary>
        /// Redirects to detail page when an item of the list tapped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void wordListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Get the list view
            ListView item = sender as ListView;

            // Get the word as the listview's selected item.
            var kelime = (Kelime)item.SelectedItem;

            // Fetch all examples of the word.
            List<Ornek> ornekList = await DBService.LoadExamples(kelime.KelimeID);

            // Redirect to detail page with the word and examples.
            await Navigation.PushModalAsync(new DetailPage(kelime, ornekList));
        }

        /// <summary>
        /// Changes background of the selected item when tapped
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                if (Application.Current.UserAppTheme == OSAppTheme.Dark)
                {
                    viewCell.View.BackgroundColor = Color.DarkRed;
                }
                else
                {
                    viewCell.View.BackgroundColor = Color.LightGray;
                }

                if (_selectedCell != null)
                    _selectedCell.View.BackgroundColor = Color.Transparent;

                _selectedCell = viewCell;
            }
        }
    }
}