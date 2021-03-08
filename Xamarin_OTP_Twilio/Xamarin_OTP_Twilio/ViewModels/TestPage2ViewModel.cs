using Plugin.Toast;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_OTP_Twilio.Interface;
using ZXing;

namespace Xamarin_OTP_Twilio.ViewModels
{
    public class TestPage2ViewModel : BindableBase
    {
        public ZXing.Result Result { get; set; }
        private ImageSource _BarcodeImageSource { get; set; }
        public ImageSource BarcodeImageSource
        {
            get { return _BarcodeImageSource; }
            set
            {
                _BarcodeImageSource = value;
                RaisePropertyChanged("BarcodeImageSource");
            }
        }

        private string _Barcode;
        public string Barcode
        {
            get { return _Barcode; }
            set { SetProperty(ref _Barcode, value); }
        }

        private bool _IsAnalyzing = true;
        public bool IsAnalyzing
        {
            get { return _IsAnalyzing; }
            set
            {
                if (!Equals(_IsAnalyzing, value))
                {
                    SetProperty(ref _IsAnalyzing, value);
                }
            }
        }

        private bool _IsScanning = true;
        public bool IsScanning
        {
            get { return _IsScanning; }
            set
            {
                if (!Equals(_IsScanning, value))
                {
                    SetProperty(ref _IsScanning, value);
                }
            }
        }
        public DelegateCommand ScanCommand => new DelegateCommand(() =>
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Barcode = Result.Text;
                CrossToastPopUp.Current.ShowToastMessage(Barcode);
                if (Barcode == "JKC")
                {

                    var stream = _barcodeService.ConvertImageStream(Barcode);
                     BarcodeImageSource = ImageSource.FromStream(() => stream);

                    //var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sig.png"); 
                    //_navigationService.NavigateAsync("/MainPage");
                }
            });
        }, CanSubmit);

        private readonly INavigationService _navigationService;
        private readonly IBarcodeService _barcodeService;
        public TestPage2ViewModel(INavigationService navigationService, IBarcodeService barcodeService)
        {
            _navigationService = navigationService;
            _barcodeService = barcodeService;
        }


        bool CanSubmit()
        {
            return true;
        }
    }
}
