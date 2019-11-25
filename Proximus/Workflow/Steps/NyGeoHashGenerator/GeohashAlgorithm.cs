using System;
using System.Linq;
using System.Collections.Generic;

namespace Proximus
{
    /// <summary>
    /// A Geohash Algorithm that generates code and also validates code. This works only for
    /// 7 digit geocodes which gives 192 meter accuracy.
    /// </summary>
    internal class GeohashAlgorithm
    {
        private WorkflowDatastore store;
        private Action<string> Log;

        /*
         * geohash length	lat bits	lng bits	lat error	lng error	    km error
                1	            2	        3	        ±23	        ±23	        ±2500
                2	            5	        5	        ±2.8	    ±5.6	    ±630
                3	            7	        8	        ±0.70	    ±0.70	    ±78
                4	            10	        10	        ±0.087	    ±0.18	    ±20
                5	            12	        13	        ±0.022	    ±0.022	    ±2.4
                6	            15	        15	        ±0.0027	    ±0.0055	    ±0.61
                7	            17	        18	        ±0.00068	±0.00068	±0.076
                8	            20	        20	        ±0.000085	±0.00017	±0.019
         */
        private static readonly int geoHashLength = 7; //geohash length
        internal GeohashAlgorithm(WorkflowDatastore datastore, Action<string> log)
        {
            this.store = datastore;
            this.Log = log;
        }
        
        internal void ComputeAndStore(string code)
        {
            if (!Valid(code))
            {
                throw new ArgumentException
                    ($"{nameof(code)} is not valid. Please refer to Geohash algorithm for allowed characters");
            }

            Compute(code);
        }

        private void Compute(string code)
        {
            if (code.Length == geoHashLength) 
            {
                var geo = new Geocode() { Code = code };
                if (!store.Exists(geo))
                {
                    store.Add(geo);
                    Log($"Added {code}");
                }
                else
                    Log($"Exists {code}");

                return;
            }

            foreach (var s in base32())
            {
                Compute(code + s);
            }

        }

        internal static bool Valid(Geocode geo)
        {
            return Valid(geo.Code);
        }

        private static bool Valid(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;

            return code.ToHashSet().IsProperSubsetOf(base32());
        }


        /// These are standard geo hash suffixes. Given a 3 digit geo code as mentioned above (e.x dr9)
        /// The below function returns the possible suffixes which are valid neighbouring geo hashes
        /// at a 192 meter accuracy. 

        private static HashSet<char> suffixes;
        internal static HashSet<char> base32()
        {
            if (suffixes == null)
                suffixes = new HashSet<char>(initializeSuffixes());
            return suffixes;
        }

        private static IEnumerable<char> initializeSuffixes()
        {
            for (int i = 48; i <= 57; i++)
                yield return (char)i;

            for (int i = 98; i <= 122; i++)
            {
                if (i == 'a' || i == 'i' || i == 'l' || i=='o')
                    continue;

                yield return (char)i;
            }
        }
    }
}
