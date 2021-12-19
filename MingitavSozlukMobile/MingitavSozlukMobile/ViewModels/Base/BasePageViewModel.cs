using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MingitavSozlukMobile.ViewModels.Base
{
    /// <summary>
    /// A base view model that contains main features of application pages
    /// </summary>
    /// <typeparam name="I"></typeparam>
    public class BasePageViewModel<I> : BaseViewModel<I>
        where I : class, new()
    {
        #region Properties

        /// <summary>
        /// A flag that controls custom popup visibility of the page
        /// </summary>
        private bool _customPopupVisibility;

        public bool CustomPopupVisibility
        {
            get { return _customPopupVisibility; }
            set { _customPopupVisibility = value; OnPropertyChanged(nameof(CustomPopupVisibility)); }
        }

        /// <summary>
        /// Title of the custom popup of page
        /// </summary>
        private string _customPopupTitle;

        public string CustomPopupTitle
        {
            get { return _customPopupTitle; }
            set { _customPopupTitle = value; OnPropertyChanged(nameof(CustomPopupTitle)); }
        }
        
        /// <summary>
        /// Content of the custom popup of page
        /// </summary>
        private string _customPopupContent;

        public string CustomPopupContent
        {
            get { return _customPopupContent; }
            set { _customPopupContent = value; OnPropertyChanged(nameof(CustomPopupContent)); }
        }


        #endregion


        #region Commands

        public ICommand ClosePopupCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BasePageViewModel()
        {
            // Set the clear command
            ClosePopupCommand = new Command(ClosePopup);
        }

        #endregion

        /// <summary>
        /// Displays the popup with desired message
        /// </summary>
        /// <param name="title">Popup title</param>
        /// <param name="content">Popup content</param>
        public void DisplayPopup(string title, string content)
        {
            // Set the title
            CustomPopupTitle = title;

            // Set the content
            CustomPopupContent = content;

            // Show Popup
            CustomPopupVisibility = true;
        }

        /// <summary>
        /// Close Popup
        /// </summary>
        /// <param name="obj"></param>
        private void ClosePopup()
        {
            CustomPopupVisibility = false;
        }

    }
}
