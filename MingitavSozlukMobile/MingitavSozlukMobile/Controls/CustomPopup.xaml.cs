using MingitavSozlukMobile.ViewModels.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MingitavSozlukMobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPopup : ContentView
    {
        public CustomPopup()
        {
            InitializeComponent();

            BindingContext = MainPageViewModel.Instance;
        }
    }
}