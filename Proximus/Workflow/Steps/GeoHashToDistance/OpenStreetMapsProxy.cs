using Neo4j.Driver.V1;
using Neo4jClient;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proximus
{
    public class OpenStreetMapsProxy
    {

        public OpenStreetMapsProxy()
        {

        }

        public double FindDistance(string source, string dest)
       {
            try
            {
                var geoHash = new GeoHash();
                var l1 = geoHash.Decode(source);
                var l2 = geoHash.Decode(dest);
                var distance =  OpenStreetMaps.GetDistance(l1, l2);

                return distance;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            throw new Exception();
        }
    }
}
