using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Proximus
{
    internal class DistanceCalculator : WorkflowStep
    {

        internal DistanceCalculator(WorkflowState state) : base(state)
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
            var osm = OpenStreetMaps.Instance;
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
