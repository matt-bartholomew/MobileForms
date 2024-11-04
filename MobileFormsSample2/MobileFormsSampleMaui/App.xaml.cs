using MobileFormsSampleMauiCore.Resources;
using MobileFormsSample.Views;
using CommunityToolkit.Maui.Behaviors;

namespace MobileFormsSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            _ = new MultiValidationBehavior();
            _ = new TextValidationBehavior();
            _ = new EmailValidationBehavior();
            _ = new NumericValidationBehavior();
            _ = new UriValidationBehavior();
        }
    }
}
