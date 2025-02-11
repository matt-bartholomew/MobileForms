﻿using ObjCRuntime;
using System.Diagnostics;
using UIKit;

namespace MobileFormsSample.iOS
{
    public class Program
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            try
            {
                UIApplication.Main(args, null, typeof(AppDelegate));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
