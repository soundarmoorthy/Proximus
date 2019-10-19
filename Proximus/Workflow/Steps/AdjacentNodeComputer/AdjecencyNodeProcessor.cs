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
            var s = store();
            Parallel.ForEach(s.Geocodes(), (geo) =>
            {
                var matrix = NodeNeighbour.Neighbours(geo.Code);
                Log($"Generated {matrix}");
                s.Add(matrix);
            }
            );
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
