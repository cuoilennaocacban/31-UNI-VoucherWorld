using System;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Data.Utilities
{
    public static class StaticMethods
    {
        public static double Distance(Place place, double lat1, double lon1)
        {
            if (place == null)
            {
                throw new NullReferenceException();
            }
            double lat2 = place.Latitude;
            double lon2 = place.Longitude;

            double e = (3.1415926538 * lat1 / 180);
            double f = (3.1415926538 * lon1 / 180);
            double g = (3.1415926538 * lat2 / 180);
            double h = (3.1415926538 * lon2 / 180);
            double i = (Math.Cos(e) * Math.Cos(g) * Math.Cos(f) * Math.Cos(h) +
                        Math.Cos(e) * Math.Sin(f) * Math.Cos(g) * Math.Sin(h) + Math.Sin(e) * Math.Sin(g));
            double j = (Math.Acos(i));
            double k = (6371 * j);
            return k;
        }

    }
}
