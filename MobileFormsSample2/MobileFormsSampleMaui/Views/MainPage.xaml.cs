using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.ImageSources;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Layouts;
using CommunityToolkit.Maui.Views;

namespace MobileFormsSample.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var viewModel = new FormViewerViewModel("MilkCrateForm.json");
            var popup = new FormViewPopup(viewModel);

            await Navigation.PushModalAsync(popup);

            //var formResult = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

           //Debug.WriteLine();
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            //    var viewModel = new FormViewerViewModel("CompletedMobileForm.json", true);
            //    var popup = new FormViewPopup(viewModel);

            //    var formResult = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

            //    Debug.WriteLine(JsonConvert.SerializeObject(formResult));
            //}
        }
    }
}
