using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin_OTP_Twilio.ViewModels;
using Xamarin_OTP_Twilio.Views;

namespace Xamarin_OTP_Twilio
{
    public partial class App
    {
        public static Theme AppTheme { get; set; }
        public static string GitBaseAddress { get { return "https://gist.githubusercontent.com/"; } }
        public static string PhoneCodeUri { get { return "https://gist.githubusercontent.com/iamswapnilsonar/0e1868229e98cc27a6d2e3487b44f7fa/raw/10f8979f0b1daa0e0b490137d51fb96736767a09/isd_country_code.json"; } }
        public static string Twilio_U { get { return "AC6b4d75b1ec5f03a5d715aa6497629f87"; } }
        public static string Twilio_P { get { return "95729e1a0599d5d5b4a9307f345d1409"; } }
        public static string Twilio_PNum { get { return "+16672135609"; } }
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            Sharpnado.Tabs.Initializer.Initialize(false, false);
            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<PrismTabbedPage1>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TestPage1, TestPage1ViewModel>();
            containerRegistry.RegisterForNavigation<TestPage2, TestPage2ViewModel>();
            containerRegistry.RegisterForNavigation<TestPage3, TestPage3ViewModel>();
        }
        public enum Theme
        {
            Light,
            Dark
        }
    }
}
