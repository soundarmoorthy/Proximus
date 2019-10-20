using System;
using System.Linq;
using System.Collections.Generic;

namespace Proximus
{
    internal class GeohashAlgorithm
    {
        private WorkflowDatastore store;
        private Action<string> Log;

        internal GeohashAlgorithm(WorkflowDatastore datastore, Action<string> log)
        {
            this.store = datastore;
            this.Log = log;
        }
        
        internal void ComputeAndStore(string code)
        {
            if (code.Distinct().Union(suffix()).Count() > suffix().Count())
            {
                throw new ArgumentException
                    ($"{nameof(code)} is not valid. Please refer to Geohash algorithm for allowed characters");
            }
            else
            {
                Compute(code);
            }
            Compute(code);
        }

        private void Compute(string code)
        {
            if (code.Length == 7) //7  = 192 meter accuracy geohashes generation
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

            foreach (var s in suffix())
            {
                Compute(code + s);
            }

        }


        /// These are standard geo hash suffixes. Given a 3 digit geo code as mentioned above (e.x dr9)
        /// The below function returns the possible suffixes which are valid neighbouring geo hashes
        /// at a 192 meter accuracy. 

        private static List<char> suffixes;
        internal static IEnumerable<char> suffix()
        {
            if (suffixes == null)
                suffixes = new List<char>(initializeSuffixes());
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
