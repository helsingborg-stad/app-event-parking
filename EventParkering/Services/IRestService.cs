using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EventParkering.Model;

namespace EventParkering.Services
{
    public interface IRestService
    {
        Task<List<EventItem>> EventDataAsync();
        //Task<ObservableCollection<ParkItem>> ParkDataAsync(int id, string dist);
    }
}