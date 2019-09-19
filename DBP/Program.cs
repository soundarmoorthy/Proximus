
using System.Collections.Generic;

namespace Proximus
{
    class Program
    {
        static Logger logger = new Logger(new[] { new ConsoleLoggerSink() });
        static void Main(string[] args)
        {
            WorkflowState state = new ProximusWorkflowState(logger, new WorkflowDatastore("."));
            var workflow = new Workflow(steps(state));
            workflow.StartAndWait();
        }

        private static IEnumerable<WorkflowStep> steps(WorkflowState state)
        {
            //The order of the steps matters
            yield return new NyGeoHashGenerator(state);
            //yield return new DistanceCalculator(state);
            //yield return new Neo4jFileCreator(state);
        }
    }
}
