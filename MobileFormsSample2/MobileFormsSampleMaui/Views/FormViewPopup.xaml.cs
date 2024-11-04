using System;
using System.Collections.Generic;
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
    public partial class FormViewPopup : ContentPage
    {
        public FormViewPopup(FormViewerViewModel vm)
        {
            BindingContext = vm;
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var vm = this.BindingContext as FormViewerViewModel;

            //Dismiss(vm.Form);

            Navigation.PopAsync();
        }
    }
}
