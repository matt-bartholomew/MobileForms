using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace MobileFormsSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //InvalidateMeasure();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var popup = new FormViewPopup();
            var viewModel = new FormViewerViewModel("MilkCrateForm.json");

            popup.BindingContext = viewModel;

            var formResult = await Navigation.ShowPopupAsync(popup);

            Debug.WriteLine(JsonConvert.SerializeObject(formResult));
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            var popup = new FormViewPopup();
            var viewModel = new FormViewerViewModel("CompletedMobileForm.json", true);

            popup.BindingContext = viewModel;

            var formResult = await Navigation.ShowPopupAsync(popup);

            Debug.WriteLine(JsonConvert.SerializeObject(formResult));
        }
    }
}
