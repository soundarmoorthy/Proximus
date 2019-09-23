using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Proximus
{
    public class DistanceCalculator : WorkflowStep
    {
        
        public DistanceCalculator(WorkflowState state) : base(state)
        {

        }

        public override void Start()
        {
            this.Process();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override string Name => "Calculate Distance";

        private void Process()
        {

            var store = this.State.Store;
            var osm = new OpenStreetMapsProxy();
            //Parallel.ForEach(store.GeocodeMatrices(), (m) =>
            foreach (var m in store.GeocodeMatrices())
            {
                foreach (var n in f(m))
                {
                    var c = m.GeoCode.Code;
                    var d = osm.FindDistance(c, n);
                    store.Add(GeoDistance.Create(c, n, d));
                }
            }
            //});
        }

        public IEnumerable<string> f(GeocodeMatrix m) => m.Neighbours().Select(x => x.Code);

    }
}
