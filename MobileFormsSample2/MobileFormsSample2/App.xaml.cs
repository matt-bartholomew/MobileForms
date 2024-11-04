using System;
using MobileFormsSample.Resources;
using Xamarin.CommunityToolkit.Behaviors;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileFormsSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            _ = new MultiValidationBehavior();
            _ = new TextValidationBehavior();
            _ = new EmailValidationBehavior();
            _ = new NumericValidationBehavior();
            _ = new UriValidationBehavior();
        }

        protected override void OnStart()
        {
            LocalizationResourceManager.Current.Init(AppResources.ResourceManager);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
