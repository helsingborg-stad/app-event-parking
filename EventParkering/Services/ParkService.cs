using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using EventParkering.Model;
using Newtonsoft.Json;

namespace EventParkering.Services
{
    public class ParkService
    {
        HttpClient client;

        public ParkService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<ObservableCollection<ParkItem>> ParkDataAsync(int id, string dist)
        {
            string callUrl = "https://labs-api-ep-prod-webbapp.azurewebsites.net/getEventParkingLots?id=" + id + "&dist=" + dist;
            try
            {
                var response = await client.GetAsync(callUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ObservableCollection<ParkItem> stations = JsonConvert.DeserializeObject<ObservableCollection<ParkItem>>(content);
                    Debug.WriteLine(content);
                    return stations;
                }
                else
                {
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }
    }
}
