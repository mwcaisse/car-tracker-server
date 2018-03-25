using System;
using System.Collections.Generic;
using System.Text;

namespace CarTracker.Logic.Util
{
    internal static class GeographyUtils
    {

        // TODO: Move the Latitude/Longitude calculates to their own utility class
        private const double EarthRadidus = 6371.0;

        // Semi-axes of WGS-84 geoidal reference
        private const double WGS84_a = 6378137.0; // Major semiaxis [m]
        private const double WGS84_b = 6356752.3; // Minor semiaxis [m]

        internal class LocationModel
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
        
        internal class BoundingBox
        {
            public LocationModel MinPoint { get; set; }
            public LocationModel MaxPoint { get; set; }
        }

        internal static double ToRadians(double degrees)
        {
            return (Math.PI / 180.0) * degrees;
        }

        internal static double ToDegrees(double radians)
        {
            return 180.0 * radians / Math.PI;
        }

        internal static double CalculateDistanceBetweenLocations(LocationModel start, LocationModel end)
        {
            return CalculateDistanceBetweenLocations(start.Latitude, start.Longitude, end.Latitude, end.Longitude);   
        }

        internal static double CalculateDistanceBetweenLocations(double startLatitude, double startLongitude,
            double endLatitude, double endLongitude)
        {
            double startLat = ToRadians(startLatitude);
            double endLat = ToRadians(endLatitude);
            double deltaLat = ToRadians(endLatitude - startLatitude);
            double deltaLong = ToRadians(endLongitude - startLongitude);

            double a = Math.Pow(Math.Sin(deltaLat / 2.0), 2.0) +
                       Math.Pow(Math.Sin(deltaLong / 2.0), 2.0) *
                       Math.Cos(startLat) * Math.Cos(endLat);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            double distance = EarthRadidus * c;

            return Math.Abs(distance);
        }

        /// <summary>
        ///  Calculate a bounding box around a point
        /// 
        ///     Adopted from https://stackoverflow.com/questions/238260/how-to-calculate-the-bounding-box-for-a-given-lat-lng-location
        /// </summary>
        /// <param name="point">Location</param>
        /// <param name="halfSide">Half of the side of the resulting square in meters</param>
        /// <returns></returns>

        internal static BoundingBox CalculateBoxAroundPoint(LocationModel point, double halfSide)
        {
            return CalculateBoxAroundPoint(point.Latitude, point.Longitude, halfSide);
        }

        internal static BoundingBox CalculateBoxAroundPoint(double latitude, double longitude, double halfSide)
        {
            var lat = ToRadians(latitude);
            var lon = ToRadians(longitude);

            // Radius of the Earth at given latitude
            var radius = WGS84EarthRadius(lat);
            // Radius of the parallel at given latitude
            var pradius = radius * Math.Cos(lat);

            var latMin = lat - halfSide / radius;
            var latMax = lat + halfSide / radius;
            var lonMin = lon - halfSide / pradius;
            var lonMax = lon + halfSide / pradius;

            return new BoundingBox()
            {
                MinPoint = new LocationModel()
                {
                    Latitude = ToDegrees(latMin),
                    Longitude = ToDegrees(lonMin)
                },
                MaxPoint = new LocationModel()
                {
                    Latitude = ToDegrees(latMax),
                    Longitude = ToDegrees(lonMax)
                }
            };
        }

        internal static double WGS84EarthRadius(double lat)
        {
            // http://en.wikipedia.org/wiki/Earth_radius
            var an = WGS84_a * WGS84_a * Math.Cos(lat);
            var bn = WGS84_b * WGS84_b * Math.Sin(lat);
            var ad = WGS84_a * Math.Cos(lat);
            var bd = WGS84_b * Math.Sin(lat);
            return Math.Sqrt((an * an + bn * bn) / (ad * ad + bd * bd));
        }

    }
}
