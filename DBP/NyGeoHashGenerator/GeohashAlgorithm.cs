using System;
using System.Collections.Generic;

namespace Proximus
{
    public class GeohashAlgorithm
    {
        private WorkflowDatastore store;
        private Action<string> Log;

        public GeohashAlgorithm(WorkflowDatastore datastore, Action<string> log)
        {
            this.store = datastore;
            this.Log = log;
        }

        public void Compute(string code)
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
        public IEnumerable<char> suffix()
        {
            for (int i = 48; i <= 57; i++)
                yield return (char)i;

            for (int i = 97; i <= 122; i++)
                yield return (char)i;
        }
    }
}
