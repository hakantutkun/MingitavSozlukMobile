using MingitavSozlukMobile.ViewModels.Base;

namespace MingitavSozlukMobile.ViewModels.Controls
{
    public class CustomPopupViewModel : BaseViewModel<CustomPopupViewModel>
    {
        #region Properties

        #endregion

        #region Commands

        //public ICommand ClosePopupCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Consturtor
        /// </summary>
        public CustomPopupViewModel()
        {
            // Set the clear command
           // ClosePopupCommand = new Command(ClosePopup);
        }

        ///// <summary>
        ///// Close Popup
        ///// </summary>
        ///// <param name="obj"></param>
        //private void ClosePopup()
        //{
        //    MainPageViewModel.Instance.CustomPopupVisibility = false;
        //}

        #endregion
    }
}
