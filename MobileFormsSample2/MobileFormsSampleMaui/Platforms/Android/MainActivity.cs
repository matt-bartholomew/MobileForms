using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using MobileFormsSample;
using Microsoft.Maui;

namespace MobileFormsSample.Droid
{
    [Activity(Label = "MobileFormsSample", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : Microsoft.Maui.MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);

            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            //global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //LoadApplication(new App());
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if (Microsoft.Maui.Devices.DeviceInfo.Version.Major >= 13 && (permissions.Any(p => p.Equals("android.permission.WRITE_EXTERNAL_STORAGE")) || permissions.Any(p => p.Equals("android.permission.READ_EXTERNAL_STORAGE"))))
            {
                var wIdx = Array.IndexOf(permissions, "android.permission.WRITE_EXTERNAL_STORAGE");
                var rIdx = Array.IndexOf(permissions, "android.permission.READ_EXTERNAL_STORAGE");

                if (wIdx != -1 && wIdx < permissions.Length) grantResults[wIdx] = Permission.Granted;
                if (rIdx != -1 && rIdx < permissions.Length) grantResults[rIdx] = Permission.Granted;
            }


            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

