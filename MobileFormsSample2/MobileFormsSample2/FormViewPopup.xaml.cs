using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace MobileFormsSample
{
    public partial class FormViewPopup : Popup<goRoam.MobileForms.Models.MobileForm>
    {
        public FormViewPopup()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var vm = this.BindingContext as FormViewerViewModel;

            Dismiss(vm.Form);
        }
    }
}
