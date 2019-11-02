using System;
using System.Diagnostics.CodeAnalysis;

namespace Proximus
{
    [ExcludeFromCodeCoverage]
    internal class ProximusWorkflowState : WorkflowState
    {
        internal ProximusWorkflowState(Logger logger, WorkflowDatastore store) : base(logger, store)
        {
        }
    }
}