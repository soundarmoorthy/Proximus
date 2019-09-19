using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proximus
{
    public static class OpenStreetMaps
    {
        private static int _connectionLeaseTimeoutMs = 105000;

        static OpenStreetMaps()
        {
        }
        public static async Task<string> GetDistance(Location l1, Location l2)
        {
            var sourceLatLng = l1.Lat.ToString() + "," + l1.Lng.ToString();
            var destinationLatLng = l2.Lat.ToString() + "," + l2.Lng.ToString();
            var uri = new Uri($"http://localhost:8989/route?point={sourceLatLng}&point={destinationLatLng}&weighting=fastest");
            var sp = ServicePointManager.FindServicePoint(uri);
            sp.ConnectionLeaseTimeout = _connectionLeaseTimeoutMs;

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(uri);

                var str = await response.Result.Content.ReadAsStringAsync();
                var geoDistanceResponse = JsonConvert.DeserializeObject<GeoResponse>(str);
                double distance = geoDistanceResponse.Paths?.FirstOrDefault().Distance ?? 0;
                double duration = geoDistanceResponse.Paths?.FirstOrDefault().Time ?? 0;

                if (Math.Abs(distance - double.Epsilon) < double.Epsilon)
                {
                    distance = 1; // 1 mtr in miles
                    duration = 1000; // 1 sec
                }
                duration /= 1000; //milli-sec -> sec
                return distance.ToString();
            }
        }
    }

    public class GeoResponse
    {
        public IEnumerable<Edge> Paths { get; set; }
    }

    public class Edge
    {
        public double Distance { get; set; }
        public double Time { get; set; }
    }
}
