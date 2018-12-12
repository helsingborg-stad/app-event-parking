using System;
using System.Collections.Generic;
using EventParkering.Objects;
using Xamarin.Forms.GoogleMaps;

namespace EventParkering.Objects
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }
}
