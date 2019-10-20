using System;
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

        internal void Compute(string code)
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
        internal static IEnumerable<char> suffix()
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
