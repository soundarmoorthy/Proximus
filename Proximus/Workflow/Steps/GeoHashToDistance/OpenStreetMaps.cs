using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Proximus
{
    public class OpenStreetMaps
    {



        private int _connectionLeaseTimeoutMs = 105000;

        private string osmUriFormat = "http://localhost:8989/route?point={0}&point={1}&weighting=fastest";

        private static OpenStreetMaps instance;
        public static OpenStreetMaps Instance
        {
            get
            {
                if (instance == null)
                    instance = new OpenStreetMaps();
                return instance;
            }
        }

        Geohash geoHash;
        private OpenStreetMaps()
        {
            geoHash = new Geohash();
        }

        public double GetDistance(string source, string dest)
        {

            var l1 = geoHash.Decode(source);
            var l2 = geoHash.Decode(dest);
            var distance = response(l1, l2);

            if (Math.Abs(distance - double.Epsilon) < double.Epsilon)
            {
                distance = 1; // 1 mtr in miles
            }
            return distance;
        }

        private double response(Location l1, Location l2)
        {
            var uri = new Uri(string.Format(osmUriFormat, l1.ToString(), l2.ToString()));
            var sp = ServicePointManager.FindServicePoint(uri);
            sp.ConnectionLeaseTimeout = _connectionLeaseTimeoutMs;

            GeoResponse result = null;
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(uri);
                var str = response.Result.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<GeoResponse>(str);
            }
            double distance = result.Paths?.FirstOrDefault().Distance ?? 0;
            return distance;
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
