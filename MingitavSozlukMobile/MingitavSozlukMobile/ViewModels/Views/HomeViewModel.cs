using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MingitavSozlukMobile.ViewModels
{
    /// <summary>
    /// A view model class for HomeView
    /// </summary>
    public class HomeViewModel : BaseViewModel<HomeViewModel>
    {
        #region Properties

        /// <summary>
        /// Title Of The view
        /// </summary>
        private string _pageTitle;

        public string PageTitle
        {
            get { return _pageTitle; }
            set { _pageTitle = value; OnPropertyChanged(nameof(PageTitle)); }
        }

        /// <summary>
        /// Subtitle Of The view
        /// </summary>
        private string _pageSubTitle;

        public string PageSubTitle
        {
            get { return _pageSubTitle; }
            set { _pageSubTitle = value; OnPropertyChanged(nameof(PageSubTitle)); }
        }

        /// <summary>
        /// Search Text
        /// </summary>
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        /// <summary>
        /// A flag that controls listview visibility
        /// </summary>
        private bool _listviewVisibility;

        public bool ListViewVisibility
        {
            get { return _listviewVisibility; }
            set { _listviewVisibility = value; OnPropertyChanged(nameof(ListViewVisibility)); }
        }

        /// <summary>
        /// A flag that controls listview visibility
        /// </summary>
        private bool _searchButtonVisibility = true;

        public bool SearchButtonVisibility
        {
            get { return _searchButtonVisibility; }
            set { _searchButtonVisibility = value; OnPropertyChanged(nameof(SearchButtonVisibility)); }
        }

        /// <summary>
        /// A flag that controls listview visibility
        /// </summary>
        private bool _closeButtonVisibility;

        public bool CloseButtonVisibility
        {
            get { return _closeButtonVisibility; }
            set { _closeButtonVisibility = value; OnPropertyChanged(nameof(CloseButtonVisibility)); }
        }

        /// <summary>
        /// The list that keeps search results
        /// </summary>
        private ObservableCollection<Kelime> _searchResultList;

        public ObservableCollection<Kelime> SearchResultList
        {
            get { return _searchResultList; }
            set { _searchResultList = value; OnPropertyChanged(nameof(SearchResultList)); }
        }

        #endregion

        #region Commands

        /// <summary>
        /// A command for clear the search entry.
        /// </summary>
        public ICommand ClearCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public HomeViewModel()
        {
            // Set the clear command
            ClearCommand = new Command(Clear);

            // Create an instance for SearchResult list.
            SearchResultList = new ObservableCollection<Kelime>();
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Clears search entry and sets the visibilities.
        /// </summary>
        private void Clear()
        {
            // Clear the text of search entry.
            SearchText = string.Empty;

            // Show search button.
            SearchButtonVisibility = true;

            // Hide close button.
            CloseButtonVisibility = false;

            // Hide listview.
            ListViewVisibility = false;
        }

        /// <summary>
        /// Searches every time the text of the search entry changed.
        /// </summary>
        /// <param name="searchText">The content of the search entry</param>
        /// <returns></returns>
        public async Task SearchLikeAsync(string searchText)
        {
            // Check if the content of the entry is empty
            if(searchText.Length != 0)
            {
                // Clear search result list.
                SearchResultList.Clear();

                // Get the words from database with given search text.
                var returnList = await DBService.LoadWordsLike(searchText);

                // Update search result list.
                SearchResultList = new ObservableCollection<Kelime>(returnList);

                // Show listview.
                ListViewVisibility = true;

                // Hide search button.
                SearchButtonVisibility = false;
                
                // Show close button.
                CloseButtonVisibility = true;
            }
            else
            {
                // Show search button.
                SearchButtonVisibility = true;

                // Hide close button.
                CloseButtonVisibility = false;

                // Hide listview.
                ListViewVisibility = false;
            }
        }

        #endregion
    }
}
