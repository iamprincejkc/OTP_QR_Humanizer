using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Xamarin_OTP_Twilio.Interface
{
    public interface IBarcodeService
    {
         Stream ConvertImageStream(string text, int width = 300, int height = 130);
    }
}
