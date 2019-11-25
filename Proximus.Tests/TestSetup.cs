using System;
using Moq;

namespace Proximus.Tests
{
    internal class TestSetup
    {
        public TestSetup()
        {
        }

        private static readonly string MemoryStreamDB = null;
        internal static WorkflowDatastore Store()
        {

            return new WorkflowDatastore(MemoryStreamDB);
        }

        internal static WorkflowState Workflowstate(WorkflowDatastore store)
        {
            return new ProximusWorkflowState(
                new Logger(ConsoleLoggerSink.Instance)
                , store);
        }
    }
}
