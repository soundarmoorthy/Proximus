using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proximus
{
    public class NyGeoHashGenerator : WorkflowStep
    {
        WorkflowDatastore store;
        public NyGeoHashGenerator(WorkflowState state) : base (state)
        {
            store = state.Datastore;
        }

        //Geo hash prefixes for new york area
        private readonly IEnumerable<string> codes = new[] { "dr8", "dr9", "drd", "dre", "drf", "drg", "dr7", "drk", "dr5" };


        public override void Start()
        {
            Parallel.ForEach(codes, (code) =>
            {
                Generate(code);
            });
        }

        public override void Stop() { }
        

        private void Generate(string code)
        {
            if (code.Length == 7) //7  = 192 meter accuracy geohashes generation
            {
                store.Add(new GeoCode() { Code = code });
                return;
            }

            foreach (var s in suffix())
            {
                Generate(code + s);
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

        public override string Name => "Generate geo hashes for New York Area";
    }
}