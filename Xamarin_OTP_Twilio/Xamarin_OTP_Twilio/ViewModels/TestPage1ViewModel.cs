using Humanizer;
using Plugin.Toast;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Xamarin_OTP_Twilio.ViewModels
{
    public class TestPage1ViewModel : BindableBase
    {

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }

        //Datepicker
        private DateTime _Date;
        public DateTime Date { get => _Date; set => SetProperty(ref _Date, value); }

        //Timepicker
        private TimeSpan _Time;
        public TimeSpan Time { get => _Time; set => SetProperty(ref _Time, value); }

        //Entry
        private string _ToRomanText;
        private string _ToCasingText;
        private string _ToPluralSingularText;
        private string _ToOrdinalText;
        public string ToRomanText { get => _ToRomanText; set => SetProperty(ref _ToRomanText, value); }
        public string ToCasingText { get => _ToCasingText; set => SetProperty(ref _ToCasingText, value); }
        public string ToPluralSingularText { get => _ToPluralSingularText; set => SetProperty(ref _ToPluralSingularText, value); }
        public string ToOrdinalText { get => _ToOrdinalText; set => SetProperty(ref _ToOrdinalText, value); }

        //Label
        private string _ToRomanOutput;
        private string _ToCasingOutput;
        private string _ToPluralSingularOutput;
        private string _ToOrdinalOutput;
        private string _ToDateTimeOutput;
        public string ToRomanOutput { get => _ToRomanOutput; set => SetProperty(ref _ToRomanOutput, value); }
        public string ToCasingOutput { get => _ToCasingOutput; set => SetProperty(ref _ToCasingOutput, value); }
        public string ToPluralSingularOutput { get => _ToPluralSingularOutput; set => SetProperty(ref _ToPluralSingularOutput, value); }
        public string ToOrdinalOutput { get => _ToOrdinalOutput; set => SetProperty(ref _ToOrdinalOutput, value); }
        public string ToDateTimeOutput { get => _ToDateTimeOutput; set => SetProperty(ref _ToDateTimeOutput, value); }

        //Command
        public DelegateCommand ConvertToRomanCommand => new DelegateCommand(ConvertToRoman, CanSubmit);
        public DelegateCommand ConvertToWordCommand => new DelegateCommand(ConvertToWord, CanSubmit);
        public DelegateCommand ConvertToPascalCommand => new DelegateCommand(ConvertToPascal, CanSubmit);
        public DelegateCommand ConvertToCamelCommand => new DelegateCommand(ConvertToCamel, CanSubmit);
        public DelegateCommand ConvertToUnderscoreCommand => new DelegateCommand(ConvertToUnderscore, CanSubmit);
        public DelegateCommand ConvertToTitleCommand => new DelegateCommand(ConvertToTitle, CanSubmit);
        public DelegateCommand ConvertToDashCommand => new DelegateCommand(ConvertToDash, CanSubmit);
        public DelegateCommand ConvertToKebabCommand => new DelegateCommand(ConvertToKebab, CanSubmit);
        public DelegateCommand ConvertToPluralCommand => new DelegateCommand(ConvertToPlural, CanSubmit);
        public DelegateCommand ConvertToSingularCommand => new DelegateCommand(ConvertToSingular, CanSubmit);
        public DelegateCommand ConvertToOrdinalCommand => new DelegateCommand(ConvertToOrdinal, CanSubmit);
        public DelegateCommand ConvertToOrdinalWordCommand => new DelegateCommand(ConvertToOrdinalWord, CanSubmit);
        public DelegateCommand ConvertToDateCommand => new DelegateCommand(ConvertToDate, CanSubmit);
        public DelegateCommand ConvertToTimeCommand => new DelegateCommand(ConvertToTime, CanSubmit);
        public DelegateCommand ConvertToDateTimeCommand => new DelegateCommand(ConvertToDateTime, CanSubmit);

        private void ConvertToDateTime()
        {
            try
            {
                DateTimeOffset newDate = Date.ToUniversalTime() + Time;
                ToDateTimeOutput = newDate.Humanize(DateTime.UtcNow);
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToDate()
        {
            try
            {
                //ToDateTimeOutput = (On.November.The13th.In(2010).AtNoon() + 5.Minutes()).ToString("dddd, MMMM d, yyyy : h:mm:ss tt");
                ToDateTimeOutput = Date.ToOrdinalWords();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToTime()
        {
            try
            {
                ToDateTimeOutput = TimeSpan.FromMilliseconds(Time.TotalMilliseconds).Humanize(4);
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToOrdinalWord()
        {
            try
            {
                ToOrdinalOutput = int.Parse(ToOrdinalText).ToOrdinalWords();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToOrdinal()
        {
            try
            {
                ToOrdinalOutput = int.Parse(ToOrdinalText).Ordinalize();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToSingular()
        {
            try
            {
                ToPluralSingularOutput = ToPluralSingularText.Singularize(inputIsKnownToBePlural: false);
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToPlural()
        {
            try
            {
                ToPluralSingularOutput = ToPluralSingularText.Pluralize(inputIsKnownToBeSingular: false);
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToKebab()
        {
            try
            {
                ToCasingOutput = ToCasingText.Kebaberize();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToDash()
        {
            try
            {
                ToCasingOutput = ToCasingText.Dasherize();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToTitle()
        {
            try
            {
                ToCasingOutput = ToCasingText.Titleize();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToUnderscore()
        {
            try
            {
                ToCasingOutput = ToCasingText.Underscore();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToCamel()
        {
            try
            {
                ToCasingOutput = ToCasingText.Camelize();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToPascal()
        {
            try
            {
                ToCasingOutput = ToCasingText.Pascalize();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToWord()
        {
            try
            {
                ToRomanOutput = int.Parse(ToRomanText).ToWords();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        private void ConvertToRoman()
        {
            try
            {
                ToRomanOutput = int.Parse(ToRomanText).ToRoman();
            }
            catch (Exception)
            {
                CrossToastPopUp.Current.ShowToastMessage("Invalid Input");
            }
        }
        bool CanSubmit()
        {
            return true;
        }
    }
}
