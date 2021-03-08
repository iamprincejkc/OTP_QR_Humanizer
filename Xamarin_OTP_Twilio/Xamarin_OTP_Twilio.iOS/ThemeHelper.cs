using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin_OTP_Twilio.Helpers;
using Xamarin_OTP_Twilio.Interface;
using static Xamarin_OTP_Twilio.App;

[assembly: Dependency(typeof(Xamarin_OTP_Twilio.iOS.ThemeHelper))]
namespace Xamarin_OTP_Twilio.iOS
{
        public class ThemeHelper : IAppTheme
        {
            public void SetAppTheme(App.Theme theme)
            {

                SetTheme(theme);
            }
            void SetTheme(Theme mode)
            {
                if (mode == Theme.Dark)
                {
                    if (App.AppTheme == Theme.Dark)
                        return;
                    App.Current.Resources = new DarkTheme();
                }
                else
                {
                    if (App.AppTheme != Theme.Dark)
                        return;
                    App.Current.Resources = new LightTheme();
                }
                App.AppTheme = mode;
            }
        }
}