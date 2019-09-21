using System;
namespace Proximus
{
    public abstract class WorkflowState
    {
        public Logger Logger { private set;  get; }
        public WorkflowDatastore Store { get; private set; }

        public WorkflowState(Logger logger, WorkflowDatastore dataStore)
        {
            this.Store = dataStore; 
            this.Logger = logger;
        }
    }
}
