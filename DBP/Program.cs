
using System.Collections.Generic;

namespace Proximus
{
    public sealed class WorkflowRunner
    {

        static void Main(string[] args)
        {
            WorkflowRunner runner = new WorkflowRunner();
            runner.Run();

        }

        private Workflow workflow;
        private Logger logger;
        private WorkflowState state;
        public WorkflowRunner()
        {
            logger = new Logger(new[] { new ConsoleLoggerSink() });
            state = new ProximusWorkflowState(logger, new WorkflowDatastore("."));
            workflow = new Workflow(steps());

        }

        public void Run()
        {
            workflow.StartAndWait();
        }

        private IEnumerable<WorkflowStep> steps()
        {
            //The order of the steps matters
            yield return new NyGeoHashGenerator(state);
            yield return new AdjecencyNodeProcessor(state);
            //yield return new DistanceCalculator(state);
            //yield return new Neo4jFileCreator(state);
        }
    }
}
