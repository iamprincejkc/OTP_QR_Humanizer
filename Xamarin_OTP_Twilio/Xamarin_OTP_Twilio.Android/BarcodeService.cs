using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin_OTP_Twilio.Droid;
using Xamarin_OTP_Twilio.Interface;
using Environment = System.Environment;
using Path = System.IO.Path;

[assembly: Dependency(typeof(BarcodeService))]
namespace Xamarin_OTP_Twilio.Droid
{
    public class BarcodeService : IBarcodeService
    {
        public Stream ConvertImageStream(string text, int width = 500, int height = 500)
        {
            var barcodeWriter = new ZXing.Mobile.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 5
                }
            };

            barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();
            var bitmap = barcodeWriter.Write(text);

            //Bitmap to stream Convert
            var stream = BitmapToStream(bitmap);

            //Save to MyPictures    
            SaveImage(stream);

            return stream;
        }

        public MemoryStream BitmapToStream(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android
            stream.Position = 0;
            return stream;
        }

        public void SaveImage(MemoryStream stream)
        {
            //Save to MyPictures
            var imgname = "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N");
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Directory.CreateDirectory(documentsPath);
            string filePath = Path.Combine(documentsPath, imgname);
            byte[] bArray = new byte[stream.Length];
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                using (stream)
                {
                    stream.Read(bArray, 0, (int)stream.Length);
                }
                int length = bArray.Length;
                fs.Write(bArray, 0, length);
            }
        }

    }
}