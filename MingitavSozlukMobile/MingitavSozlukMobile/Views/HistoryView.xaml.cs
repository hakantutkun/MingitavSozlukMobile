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
    /// A view that contains the words that user searched before.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryView : ContentView
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HistoryView()
        {
            // Initialize the components.
            InitializeComponent();

            // Set the binding context
            BindingContext = HistoryViewModel.Instance;
        }

        /// <summary>
        /// Removes the selected word from searched list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeButton_Clicked(object sender, System.EventArgs e)
        {
            // Get the button as selected item.
            Button btn = sender as Button;

            // Get the word by using the button's binding context.
            Kelime kelime = (Kelime)btn.BindingContext;

            // Remove word from the list by using view model.
            ((HistoryViewModel)BindingContext).RemoveWord(kelime);
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
    }
}