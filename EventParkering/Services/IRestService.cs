using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventParkering.Model;

namespace EventParkering.Services
{
    public interface IRestService
    {
        Task<List<EventItem>> EventDataAsync();
    }
}