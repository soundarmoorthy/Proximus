
using System.Reflection;
using System.Threading.Tasks;

namespace Proximus
{
    public abstract class WorkflowStep
    {
        public abstract string Name { get; }

        protected WorkflowState State { private set; get; }
        public WorkflowStep(WorkflowState state)
        {
            this.State = state;
        }

        public abstract void Start();

        public abstract void Stop();

    }
}
