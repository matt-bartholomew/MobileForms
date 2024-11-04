using CommunityToolkit.Maui;
using LocalizationResourceManager.Maui;
using Microsoft.Maui.LifecycleEvents;
using MobileFormsSampleMauiCore.Resources;
using MobileFormsSample.Views;
using System.Diagnostics;

namespace MobileFormsSample;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        //AppCenter.Start("android=e0b01237-af3e-4e82-8df0-ebb24ae1c20d;ios=923451b3-01d3-4807-9eec-1ae9069c9fe9", typeof(Analytics), typeof(Crashes));

        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseLocalizationResourceManager(settings =>
            {
                settings.AddResource(AppResources.ResourceManager);
                settings.AddResource(goRoam.MobileFormsMauiCore.Resources.AppResources.ResourceManager);
                settings.RestoreLatestCulture(true);
            });

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<FormViewPopup>(); 
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<FormViewerViewModel>();

        return builder.Build();
    }

    private static void HandleNavigationError(Exception ex)
    {
        Debug.WriteLine("***********************************************");
        Exception myExc = ex;

        while (myExc!=null)
        {
            Console.WriteLine(myExc.Message);
            myExc = myExc.InnerException;
        }
        System.Diagnostics.Debugger.Break();
    }
}
