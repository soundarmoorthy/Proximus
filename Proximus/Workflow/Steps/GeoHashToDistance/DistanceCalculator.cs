using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Proximus
{
    internal class DistanceCalculator : WorkflowStep
    {

        OpenStreetMapsProxy OsmProxy;
        internal DistanceCalculator(WorkflowState state, OpenStreetMapsProxy proxy= null) : base(state)
        {
            if (proxy == null)
                OsmProxy = new OpenStreetMapsProxy();
            else
                this.OsmProxy = proxy;
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
            var osm = OsmProxy;
            Parallel.ForEach(store.GeocodeMatrices(), (m) =>
            {
                foreach (var n in f(m))
                {
                    var c = m.Geocode.Code;
                    var d = osm.FindDistance(c, n);
                    store.Add(GeoDistance.Create(c, n, d));
                }
            });
        }
        public IEnumerable<string> f(GeocodeMatrix m) => m.Neighbours().Select(x => x.Code);

    }
}
