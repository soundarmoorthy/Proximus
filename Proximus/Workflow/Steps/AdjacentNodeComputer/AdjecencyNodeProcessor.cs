using System;
using System.Threading.Tasks;

namespace Proximus
{
    public class AdjecencyNodeProcessor : WorkflowStep
    {
        public AdjecencyNodeProcessor(WorkflowState state) : base(state)
        {
        }

        public override string Name => "Find Adjecency Nodes";

        private WorkflowDatastore store() => this.State.Store;

        public override void Start()
        {
            var s = store();
            //Parallel.ForEach(s.Geocodes(), (geo) =>
            foreach (var geo in s.Geocodes())
            {
                var matrix = NodeNeighbour.Neighbours(geo.Code);
                Log($"Generated {matrix}");
                s.Add(matrix);
            }
            //);
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
