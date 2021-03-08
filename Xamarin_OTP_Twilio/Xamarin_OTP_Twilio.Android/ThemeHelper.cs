using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin_OTP_Twilio.Helpers;
using Xamarin.Forms;
using Xamarin_OTP_Twilio.Interface;
using static Xamarin_OTP_Twilio.App;

[assembly: Dependency(typeof(Xamarin_OTP_Twilio.Droid.ThemeHelper))]
namespace Xamarin_OTP_Twilio.Droid
{
    public class ThemeHelper: IAppTheme
    {
        public void SetAppTheme(Theme theme)
        {
            SetTheme(theme);
        }
        void SetTheme(Theme mode)
        {
            if (mode == Theme.Dark)
            {
                if (AppTheme == Theme.Dark)
                    return;
                App.Current.Resources = new DarkTheme();
            }
            else
            {
                if (AppTheme != Theme.Dark)
                    return;
                App.Current.Resources = new LightTheme();
            }
            AppTheme = mode;
        }
    }
}