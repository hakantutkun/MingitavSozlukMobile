using MingitavSozlukMobile.Services;
using MingitavSozlukMobile.ViewModels.Base;
using MingitavSozlukMobile.ViewModels.Pages;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MingitavSozlukMobile.ViewModels.Controls
{
    public class OneWordControlViewModel : BaseViewModel<OneWordControlViewModel>
    {
        #region Properties

        /// <summary>
        /// Random instance for creating a random number
        /// </summary>
        private Random _random;

        private Kelime _selectedWord;

        public Kelime SelectedWord
        {
            get { return _selectedWord; }
            set { _selectedWord = value; OnPropertyChanged(nameof(SelectedWord)); }
        }


        /// <summary>
        /// Main Karachay word
        /// </summary>
        private string _mainWord;

        public string MainWord
        {
            get { return _mainWord; }
            set { _mainWord = value; OnPropertyChanged(nameof(MainWord)); }
        }

        /// <summary>
        /// MeaningText
        /// </summary>
        private string _meaningText;

        public string MeaningText
        {
            get { return _meaningText; }
            set { _meaningText = value; OnPropertyChanged(nameof(MeaningText)); }
        }

        /// <summary>
        /// A flag that keeps LayoutVisibility
        /// </summary>
        private bool _layoutVisibility;

        public bool LayoutVisibility
        {
            get { return _layoutVisibility; }
            set { _layoutVisibility = value; OnPropertyChanged(nameof(LayoutVisibility)); }
        }

        /// <summary>
        /// Fav info
        /// </summary>
        private bool _isFav;

        public bool IsFav
        {
            get { return _isFav; }
            set { _isFav = value; OnPropertyChanged(nameof(IsFav)); }
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
                    LayoutVisibility = false;
                }

                OnPropertyChanged(nameof(SpinnerRunning));
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command for refreshing the word
        /// </summary>
        public ICommand RefreshWordCommand { get; set; }

        /// <summary>
        /// Command for saving the word
        /// </summary>
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructor


        /// <summary>
        /// Default Constructor
        /// </summary>
        public OneWordControlViewModel()
        {
            // Command Instance
            RefreshWordCommand = new Command(GetRandomWord);
            SaveCommand = new Command(SaveWord);

            // Generate an instance of random 
            _random = new Random();

            // Get Random Word
            GetRandomWord();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets random word from database
        /// </summary>
        private void GetRandomWord()
        {
            var randNumber = _random.Next(1, 12000);


            // Update the word in database.
            Task.Run(() =>
            {
                SelectedWord = DBService.GetOneWord(randNumber).Result;

                MainWord = SelectedWord.KaracaycaAnlam;

                MeaningText = SelectedWord.TurkceAnlam;

                IsFav = SelectedWord.IsFav == 1 ? true : false;

            }).Wait();

            SpinnerRunning = false;

            LayoutVisibility = true;

        }

        private async void SaveWord()
        {
            // Check if the word saved before
            if (SelectedWord.IsFav.Equals(1))
            {
                // If saved, remove word
                SelectedWord.IsFav = 0;

                // Set visually
                IsFav = false;

                // Update removed word
                await DBService.UpdateWord(SelectedWord);

                // Display an alert to user which indicates that word has been removed
                MainPageViewModel.Instance.DisplayPopup("Bilgi", "Kelime kaydedilen kelimlelerden çıkarıldı.");
            }
            // if word has not been saved before,
            else
            {
                // Set the word as saved
                SelectedWord.IsFav = 1;

                // Set visually
                IsFav = true;

                // Set the wword's favourite time to current time
                SelectedWord.FavTime = DateTime.Now.ToString();

                // Update the word as saved.
                await DBService.UpdateWord(SelectedWord);

                // Display an alert to user which indicates that word has been added to favourites.
                MainPageViewModel.Instance.DisplayPopup("Bilgi", "Kelime kaydedilen kelimlelere eklendi.");
            }
        }

        #endregion

    }
}
