using MingitavSozlukMobile.Pages;
using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels;
using MingitavSozlukMobile.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MingitavSozlukMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView
    {
        ObservableCollection<Kelime> wordList = new ObservableCollection<Kelime>();

        public HomeView()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();
        }

        private async void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            await ((HomeViewModel)BindingContext).SearchLikeAsync((sender as Entry).Text);
        }

        private async void wordListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView item = sender as ListView;

            var kelime = (Kelime)item.SelectedItem;

            List<Ornek> ornekList = await DBService.LoadExamples(kelime.KelimeID);

            await Navigation.PushModalAsync(new DetailPage(kelime, ornekList));
        }

        private async void CopyLabel_Tapped(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(searchEntry.Text);
            MainPageViewModel.Instance.DisplayPopup("Bilgi", "İçerik başarıyla kopyalandı.");
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                if(Application.Current.UserAppTheme == OSAppTheme.Dark)
                {
                    viewCell.View.BackgroundColor = Color.DarkGray;
                }
                else
                {
                    viewCell.View.BackgroundColor = Color.LightGray;
                }
            }
        }

        private async void AllWordsGrid_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AllWordsPage());
        }

        private async void SettingsGrid_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingsPage());
        }
    }
}