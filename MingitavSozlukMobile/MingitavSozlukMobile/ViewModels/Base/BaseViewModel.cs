using System.ComponentModel;

namespace MingitavSozlukMobile.ViewModels.Base
{
    /// <summary>
    /// A Base ViewModel for all ViewModel Classes
    /// </summary>
    public class BaseViewModel<I> : INotifyPropertyChanged where I : class, new()
    {
        #region Singleton Implementation

        /// <summary>
        /// Singleton lock object
        /// </summary>
        private static readonly object lockObj = new object();

        /// <summary>
        /// Singleton instance
        /// </summary>
        private static I _instance;

        /// <summary>
        /// Instance Property
        /// </summary>
        public static I Instance
        {
            get
            {
                lock (lockObj)
                {
                    // Check null reference
                    if (_instance == null)
                    {
                        // Create a new instance if it is null
                        _instance = new I();
                    }
                    return _instance;
                }
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// INotifyPropertyChanged event implementation
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Helper Methods

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
