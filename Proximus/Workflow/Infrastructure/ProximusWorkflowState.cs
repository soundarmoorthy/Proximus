using System;
namespace Proximus
{
    internal class ProximusWorkflowState : WorkflowState
    {
        internal ProximusWorkflowState(Logger logger, WorkflowDatastore store) : base(logger, store)
        {
        }
    }
}