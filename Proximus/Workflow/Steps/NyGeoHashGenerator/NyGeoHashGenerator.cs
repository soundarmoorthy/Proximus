using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Proximus
{
    internal class NyGeoHashGenerator : WorkflowStep
    {
        WorkflowDatastore store;
        GeohashAlgorithm algorithm;
        internal NyGeoHashGenerator(WorkflowState state) : base (state)
        {
            algorithm = new GeohashAlgorithm(state.Store, this.Log);
            store = state.Store;
        }

        //Geo hash prefixes for new york area
        private readonly IEnumerable<string> codes = new[] { "dr8", "dr9", "drd", "dre", "drf", "drg", "dr7", "drk", "dr5" };

        public override void Start()
        {
          var result =   Parallel.ForEach(codes, (code, loopCtl) =>
            {
                algorithm.Compute(code);
            });
        }

        public override void Stop() { }
        
        public override string Name => "Generate geo hashes for New York Area";
    }
}