using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MingitavSozlukMobile.ViewModels
{
    /// <summary>
    /// A view model class for FavouritesView
    /// </summary>
    public class FavouritesViewModel : BaseViewModel<FavouritesViewModel>
    {
       
        #region Properties

        /// <summary>
        /// The list that keeps search results
        /// </summary>
        private ObservableCollection<Kelime> _favouritesList;

        public ObservableCollection<Kelime> FavouritesList
        {
            get { return _favouritesList; }
            set { _favouritesList = value; OnPropertyChanged(nameof(FavouritesList)); }
        }

        /// <summary>
        /// A flag that keeps listviewvisibility
        /// </summary>
        private bool _listViewVisibility;

        public bool ListViewVisibility
        {
            get { return _listViewVisibility; }
            set { _listViewVisibility = value; OnPropertyChanged(nameof(ListViewVisibility)); }
        }

        /// <summary>
        /// A flag that keeps running state of spinner
        /// </summary>
        private bool _spinnerRunning = true;

        public bool SpinnerRunning
        {
            get { return _spinnerRunning; }
            set
            {
                _spinnerRunning = value;

                if (value)
                {
                    ListViewVisibility = false;
                }

                OnPropertyChanged(nameof(SpinnerRunning));
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// A command for delete search histroy
        /// </summary>
        public ICommand DeleteHistoryCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FavouritesViewModel()
        {
            DeleteHistoryCommand = new Command(DeleteHistory);

            // Create an instance for FavouritesList.
            FavouritesList = new ObservableCollection<Kelime>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the favourite words from database.
        /// </summary>
        public void GetWords()
        {
            SpinnerRunning = true;

            Task.Run(() =>
            {
                Thread.Sleep(300);

                // Load the words from database.
                var returnList = DBService.LoadFavouriteWords().Result;

                // Set the loaded words to the list.
                FavouritesList = new ObservableCollection<Kelime>(returnList);

                // Set listview visibility to true
                ListViewVisibility = true;

                // Stop spinner running state
                SpinnerRunning = false;

            });
        }

        /// <summary>
        /// Removes the selected word from the favourites list.
        /// </summary>
        /// <param name="kelime">Selected Word</param>
        public void RemoveWord(Kelime kelime)
        {
            // Set the word's favourite flag to false.
            kelime.IsFav = 0;

            // Update the word.
            Task.Run(() =>
            {
                DBService.UpdateWord(kelime).Wait();

            }).Wait();

            // Remove the word from the list.
            FavouritesList.Remove(kelime);

        }

        /// <summary>
        /// Deletes all words from history
        /// </summary>
        private void DeleteHistory()
        {
            foreach (var item in FavouritesList)
            {
                // Update the word in database.
                Task.Run(() =>
                {
                    // Set the word's searched flag to the false.
                    item.IsFav = 0;

                    DBService.UpdateWord(item).Wait();

                }).Wait();
            }

            FavouritesList.Clear();
        }

        #endregion
    }
}
