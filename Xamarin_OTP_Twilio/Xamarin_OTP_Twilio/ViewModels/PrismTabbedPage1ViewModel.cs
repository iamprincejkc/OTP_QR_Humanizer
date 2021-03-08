using Prism.Mvvm;
using Xamarin.Essentials;
using Xamarin_OTP_Twilio.Interface;
using static Xamarin_OTP_Twilio.App;

namespace Xamarin_OTP_Twilio.ViewModels
{
    public class PrismTabbedPage1ViewModel : BindableBase
    {
        private readonly IAppTheme _appTheme;
        public PrismTabbedPage1ViewModel(IAppTheme appTheme)
        {
            _appTheme = appTheme;

            var theme = Preferences.Get("Theme", "Light");
            if (theme == "Light")
            {
                _appTheme.SetAppTheme(Theme.Light);
            }
            else
            {
                _appTheme.SetAppTheme(Theme.Dark);
            }
        }
    }
}
