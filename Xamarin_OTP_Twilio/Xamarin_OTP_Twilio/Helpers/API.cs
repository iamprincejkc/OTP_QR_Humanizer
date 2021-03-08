using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_OTP_Twilio.Helpers
{
    public class API
    {
        public ObservableCollection<Country> GetCountryPhoneCode()
        {

            ObservableCollection<Country> Countries = new ObservableCollection<Country>();
            Uri uri = new Uri(App.PhoneCodeUri);
            //bypass SSL Cert.
            //start
            var handler = new HttpClientHandler()
            { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; } };
            //end

            
            using(var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(App.GitBaseAddress);
                HttpResponseMessage response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    string content =  response.Content.ReadAsStringAsync().Result;
                    Countries = JsonConvert.DeserializeObject<ObservableCollection<Country>>(content);
                }
            }
            handler.Dispose();



            return Countries;
        }
    }
}
