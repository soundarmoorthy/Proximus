using System;
namespace Proximus
{
    
    internal abstract class WorkflowState
    {
        public Logger Logger { private set;  get; }
        public WorkflowDatastore Store { get; private set; }

        internal WorkflowState(Logger logger, WorkflowDatastore dataStore)
        {
            this.Store = dataStore; 
            this.Logger = logger;
        }
    }
}
