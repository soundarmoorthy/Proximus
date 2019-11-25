using System;
using System.Threading.Tasks;

namespace Proximus
{
    internal class AdjecencyNodeProcessor : WorkflowStep
    {
        internal AdjecencyNodeProcessor(WorkflowState state) : base(state)
        {
        }

        public override string Name => "Find Adjecency Nodes";

        private WorkflowDatastore store() => this.State.Store;

        public override void Start()
        {
            foreach (var geo in store().Geocodes())
            {
                Process(geo);
            }
        }

        private void Process(Geocode geo)
        {
            if (!GeohashAlgorithm.Valid(geo))
                return;

            var matrix = GeohashNeighbours.Compute(geo.Code);
            Log($"Generated {matrix}");
            store().Add(matrix);
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}