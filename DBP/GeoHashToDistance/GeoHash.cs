using System;

namespace Proximus
{
    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class GeoHash
    {
        private string _base32 = "0123456789bcdefghjkmnpqrstuvwxyz";
        public Bound GetBound(string geohash)
        {
            if (geohash.Length == 0) throw new Exception("Invalid geohash");

            geohash = geohash.ToLower();

            var evenBit = true;
            double latMin = -90, latMax = 90;
            double lonMin = -180, lonMax = 180;

            for (var i = 0; i < geohash.Length; i++)
            {
                var chr = geohash[i];
                var idx = _base32.IndexOf(chr);
                if (idx == -1) throw new Exception("Invalid geohash");

                for (var n = 4; n >= 0; n--)
                {
                    var bitN = idx >> n & 1;
                    if (evenBit)
                    {
                        // longitude
                        var lonMid = (lonMin + lonMax) / 2;
                        if (bitN == 1)
                        {
                            lonMin = lonMid;
                        }
                        else
                        {
                            lonMax = lonMid;
                        }
                    }
                    else
                    {
                        // latitude
                        var latMid = (latMin + latMax) / 2;
                        if (bitN == 1)
                        {
                            latMin = latMid;
                        }
                        else
                        {
                            latMax = latMid;
                        }
                    }
                    evenBit = !evenBit;
                }
            }

            var bounds = new Bound
            {
                SW = new Location { Lat = latMin, Lng = lonMin },
                NE = new Location { Lat = latMax, Lng = lonMax },
            };

            return bounds;
        }

        public Location Decode(string geohash)
        {

            var bounds = GetBound(geohash); // <-- the hard work
                                            // now just determine the centre of the cell...

            double latMin = bounds.SW.Lat, lonMin = bounds.SW.Lng;
            double latMax = bounds.NE.Lat, lonMax = bounds.NE.Lng;

            // cell centre
            var lat = (latMin + latMax) / 2;
            var lon = (lonMin + lonMax) / 2;

            //// round to close to centre without excessive precision: ⌊2-log10(Δ°)⌋ decimal places
            //lat = lat.ToFixed(Math.Floor(2 - Math.Log(latMax - latMin) / Math.Log(10)));
            //lon = lon.toFixed(Math.Floor(2 - Math.Log(lonMax - lonMin) / Math.Log(10)));

            return new Location { Lat = lat, Lng = lon };
        }
    }

    public class Bound
    {
        public Location SW { get; set; }
        public Location NE { get; set; }
    }

    public static class MyExtensionMethods
    {
        public static string ToFixed(this double number, double decimals)
        {
            return number.ToString("N" + decimals);
        }
    }
}
