using Newtonsoft.Json;
using Plugin.Toast;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin_OTP_Twilio.Helpers;
using Xamarin_OTP_Twilio.Interface;
using static Xamarin_OTP_Twilio.App;

namespace Xamarin_OTP_Twilio.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IActiveAware
    {


        API api = new API();
        public event EventHandler IsActiveChanged;


        private bool _BusyIndicator;
        public bool BusyIndicator
        {
            get { return _BusyIndicator; }
            set { SetProperty(ref _BusyIndicator, value); }
        }
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetProperty(ref _IsActive, value, RaisePropertyChanged); }
        }
        private bool _IsToggled;
        public bool IsToggled
        {
            get { return _IsToggled; }
            set { SetProperty(ref _IsToggled, value, RaisePropertyChanged); }
        }
        private string _Telephone;
        public string Telephone
        {
            get { return _Telephone; }
            set { SetProperty(ref _Telephone, value); }
        }
        private string _Otp;
        public string Otp
        {
            get { return _Otp; }
            set { SetProperty(ref _Otp, value); }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        private Country _SelectedCountries;
        public Country SelectedCountries
        {
            get { return _SelectedCountries; }

            set { SetProperty(ref _SelectedCountries, value, RaisePropertyChanged); }
        }
        private ObservableCollection<Country> _Countries;
        public ObservableCollection<Country> Countries
        {
            get { return _Countries; }
            set
            {
                _Countries = value;
                RaisePropertyChanged("Countries");
            }
        }


        public DelegateCommand SendOTPCommand => new DelegateCommand(SendOtp, CanSubmit);
        public DelegateCommand ResendOTPCommand => new DelegateCommand(ResendOTP, CanSubmit);
        public DelegateCommand VerifyOTPCommand => new DelegateCommand(VerifyOtp, CanSubmit);
        public DelegateCommand RefreshViewCommand => new DelegateCommand(RefreshData, CanSubmit);
        public DelegateCommand ToggleCommand => new DelegateCommand(Toggle, CanSubmit);


        private string getotp { get; set; }
        private readonly IAppTheme _appTheme;


        public MainPageViewModel(INavigationService navigationService, IAppTheme appTheme)
            : base(navigationService)
        {

            Telephone = "9957540949";
            getotp = "";

            _appTheme = appTheme;



            Task.Run(async delegate
            {
                //await Task.Delay(1000);

                var theme = Preferences.Get("Theme", "Light");
                if (theme == "Light")
                {
                    _appTheme.SetAppTheme(Theme.Light);
                    IsToggled = false;
                }
                else
                {
                    _appTheme.SetAppTheme(Theme.Dark);
                    IsToggled = true;
                }

                await Task.Delay(2000);

                LoadData();

            });


        }
        public virtual void RaisePropertyChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
        bool CanSubmit()
        {
            return true;
        }
        private void Toggle()
        {
            SetTheme(IsToggled);
        }
        void SetTheme(bool status)
        {
            Theme themeRequested;
            if (status)
            {
                themeRequested = Theme.Dark;
                Preferences.Set("Theme", "Dark");
            }
            else
            {
                themeRequested = Theme.Light;
                Preferences.Set("Theme", "Light");
            }
            _appTheme.SetAppTheme(themeRequested);
        }
        private void RefreshData()
        {
            LoadData();
            this.IsRefreshing = false;
        }
        public async void LoadData()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (current == NetworkAccess.Internet)
                {


                    var response = api.GetCountryPhoneCode();

                    if(response.Count>0)
                    {
                        Countries = response;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Cant Load Data", " Please Check Internet Connection ", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
        private void VerifyOtp()
        {
            try
            {
                if (getotp != Otp) //
                {
                    BusyIndicator = true;
                    {
                        NavigationService.NavigateAsync("/NavigationPage/PrismTabbedPage1");
                        BusyIndicator = false;
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Verification Failed", " wrong OTP ", "OK");
                }
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Verification Failed", " wrong OTP ", "OK");
            }
        }
        private void ResendOTP()
        {
            SendOtp();
        }
        private void SendOtp()
        {
            try
            {
                TwilioClient.Init(App.Twilio_U, App.Twilio_P);
                
                Random generator = new Random();
                getotp = generator.Next(0, 99999).ToString("D5");

                var message = MessageResource.Create(
            body: "Hello, Your new OTP is " + getotp,
            from: new Twilio.Types.PhoneNumber(App.Twilio_PNum),
            to: new Twilio.Types.PhoneNumber(SelectedCountries.dial_code + Telephone)
                 );


                CrossToastPopUp.Current.ShowToastMessage(" OTP sent ");
            }
            catch (Exception)
            {
                Application.Current.MainPage.DisplayAlert("Failed", " OTP not sent ", "OK");
            }

        }
    }
}
