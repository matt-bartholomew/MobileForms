using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace goRoam.MobileForms.Controls.Questions
{
    public partial class SignatureCapturePopop : Popup
    {
        public SignatureCapturePopop()
        {
            InitializeComponent();
        }

        void cancelButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Dismiss(new byte[] { });
        }

        async void applyButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var sigStream = await signaturePad.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);

            var bytes = ResizeSignature(sigStream);

            Dismiss(bytes);
        }

        private byte[] ResizeSignature(Stream originalImage)
        {
            var bitmap = SkiaSharp.SKBitmap.FromImage(SkiaSharp.SKImage.FromEncodedData(originalImage));

            var height = bitmap.Height;
            var width = bitmap.Width;
            var scaleFactor = 125.0/ height;
            var newWidth = Convert.ToInt32(width * scaleFactor);

            var resized = bitmap.Resize(new SkiaSharp.SKImageInfo(newWidth, 125), SkiaSharp.SKFilterQuality.High);
            var newImage = SkiaSharp.SKImage.FromBitmap(resized);
            var png = newImage.Encode();
            return png.ToArray();
        }
    }
}
