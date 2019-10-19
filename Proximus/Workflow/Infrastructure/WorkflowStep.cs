
using System;
using System.Collections.Concurrent;

using System.Reflection;
using System.Threading.Tasks;

namespace Proximus
{
    internal abstract class WorkflowStep
    {
        public abstract string Name { get; }
        Logger logger;
        protected WorkflowState State { private set; get; }

        internal WorkflowStep(WorkflowState state)
        {
            this.State = state;
            this.logger = state.Logger;
        }

        public abstract void Start();

        public abstract void Stop();

        public void Log(string messgae)
        {
            logger.Log($"{Name}-{DateTime.UtcNow}-{messgae}");
        }

    }
}
