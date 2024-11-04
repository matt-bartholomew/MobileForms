using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class PicturePopup : Popup
    {
        public PicturePopup()
        {
            InitializeComponent();
        }

        void takePictureButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var photo = await MediaPicker.CapturePhotoAsync();

                    if (photo == null)
                    {
                        throw new NullReferenceException("photo does not exist");
                    }

                    using (var stream = await photo.OpenReadAsync())
                        Dismiss(ResizeSignature(stream));
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    Dismiss(new byte[] { });
                    // Feature is not supported on the device
                }
                catch (PermissionException pEx)
                {
                    Dismiss(new byte[] { });
                    // Permissions not granted
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                    Dismiss(new byte[] { });
                }
            });
        }

        void pickPictureButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var photo = await MediaPicker.PickPhotoAsync();

                    using (var stream = await photo.OpenReadAsync())
                        Dismiss(ResizeSignature(stream));
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    Dismiss(new byte[] { });
                    // Feature is not supported on the device
                }
                catch (PermissionException pEx)
                {
                    Dismiss(new byte[] { });
                    // Permissions not granted
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                    Dismiss(new byte[] { });
                }
            });
        }

        void cancelButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Dismiss(new byte[] { });
        }

        private byte[] ResizeSignature(Stream originalImage)
        {
            var bitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromEncodedData(originalImage));

            var height = bitmap.Height;
            var width = bitmap.Width;
            var scaleFactor = 800.0 / height;
            var newWidth = Convert.ToInt32(width * scaleFactor);

            var resized = bitmap.Resize(new SkiaSharp.SKImageInfo(newWidth, 800), SkiaSharp.SKFilterQuality.High);
            var newImage = SkiaSharp.SKImage.FromBitmap(resized);
            var png = newImage.Encode();
            return png.ToArray();
        }

        void clearPictureButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Dismiss(null);
        }
    }
}
