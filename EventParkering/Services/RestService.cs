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
        static readonly Lazy<RestService> _instanceHolder =
                  new Lazy<RestService>(() => new RestService());

        public List<EventItem> EventList { get; private set; }
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public static RestService Instance => _instanceHolder.Value;

        public async Task<List<EventItem>> RefreshDataAsync()
        {

            EventList = new List<EventItem>();
            var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

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
    }
}