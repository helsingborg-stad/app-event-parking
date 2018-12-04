﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using EventParkering.Model;
using Newtonsoft.Json;

namespace EventParkering.Services
{
    public class RestService : IRestService
    {
        public List<EventItem> EventList { get; private set; }
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<EventItem>> EventDataAsync()
        {
            EventList = new List<EventItem>();

            var uri = new Uri(string.Format(Constants.EventUrl, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    EventList = JsonConvert.DeserializeObject<List<EventItem>>(content);
                    Debug.WriteLine(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return EventList;
        }

        public async Task ParkDataAsync()
        {
            try
            {
                var uri = new Uri(string.Format(Constants.ParkUrl, string.Empty));
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine("\nException Caught!");
                Debug.WriteLine("Message :{0} ", e.Message);
            }
            client.Dispose();
        }
    }
}