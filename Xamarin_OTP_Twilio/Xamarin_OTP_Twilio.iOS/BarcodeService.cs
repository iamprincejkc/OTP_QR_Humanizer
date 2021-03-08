using Foundation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin_OTP_Twilio.Interface;

[assembly: Dependency(typeof(Xamarin_OTP_Twilio.iOS.BarcodeService))]
namespace Xamarin_OTP_Twilio.iOS
{
    public class BarcodeService: IBarcodeService
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

            //Convert Stream to MemoryStream
            //var memstream = new MemoryStream();
            //stream.CopyTo(memstream);

            //Save to MyPictures
            SaveImage(stream);

            return stream;
        }



        public Stream BitmapToStream(UIImage bitmap)
        {
            var stream = bitmap.AsPNG().AsStream(); // this is the difference 
            stream.Position = 0;
            return stream;
        }


        public void SaveImage(Stream stream)
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