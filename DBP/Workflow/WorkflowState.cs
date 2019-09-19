using System;
namespace Proximus
{
    public abstract class WorkflowState
    {
        public Logger Logger { private set;  get; }
        public WorkflowDatastore Datastore { get; private set; }

        public WorkflowState(Logger logger, WorkflowDatastore dataStore)
        {
            this.Datastore = dataStore; 
            this.Logger = logger;
        }
    }
}
