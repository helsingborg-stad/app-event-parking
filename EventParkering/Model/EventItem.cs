using System;
using System.ComponentModel;

namespace EventParkering.Model
{
    public class EventItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string streetAddress { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }       
    }
}
