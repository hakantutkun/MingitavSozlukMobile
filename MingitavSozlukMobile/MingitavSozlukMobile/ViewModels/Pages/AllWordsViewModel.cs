using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace MingitavSozlukMobile.ViewModels.Pages
{
    public class AllWordsViewModel : BasePageViewModel<AllWordsViewModel>
    {
        #region Properties

        /// <summary>
        /// The list that keeps all words
        /// </summary>
        private ObservableCollection<Kelime> _wordList;

        public ObservableCollection<Kelime> WordList
        {
            get { return _wordList; }
            set { _wordList = value; OnPropertyChanged(nameof(WordList)); }
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

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AllWordsViewModel()
        {
            // Create an instance for history list.
            WordList = new ObservableCollection<Kelime>();

            // Get words when page loads.
            GetWords();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the history words from the database.
        /// </summary>
        public void GetWords()
        {
            SpinnerRunning = true;

            // Get the words from database.
            Task.Run(() =>
            {
                Thread.Sleep(300);

                // Get the words from database service.
                var returnList = DBService.LoadAllWords().Result;

                // Set the fetched words to the history list.
                WordList = new ObservableCollection<Kelime>(returnList);

                // Set listview visibility to true
                ListViewVisibility = true;

                // Stop spinner running state
                SpinnerRunning = false;

            });
        }

        #endregion
    }
}
