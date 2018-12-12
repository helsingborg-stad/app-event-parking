using System;
using System.Threading.Tasks;

namespace EventParkering
{
    public static class CalculateDistance
    {
        public static double CaculateMidPoint (double latt1, double lon1, double eventlat, double eventlon)
        {
            double R = 6371.0; // Earth's radius
            var dLat = (Math.PI / 180) * (latt1 - eventlat);
            var dLon = (Math.PI / 180) * (lon1 - eventlon);
            var lat1 = (Math.PI / 180) * eventlat;
            var lat2 = (Math.PI / 180) * lat1;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // distance in Km.
            return d;
        }

        //calculate distance between the points
        public static double CalculateTheDistance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
            Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'C')
            {
                dist = dist * 160934.4;
            }

            return dist;
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }

}
