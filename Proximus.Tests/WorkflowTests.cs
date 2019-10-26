using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Proximus.Tests
{
    [TestClass]
    public class WorkflowTests
    {
        public WorkflowTests()
        {
        }

        [TestMethod]
        public void StartAndWait_Executes_All_Steps_And_Wait_For_Completion()
        {
            var step1 = new Mock<WorkflowStep>();
            step1.Setup(x => x.Start()).Verifiable();

            var step2 = new Mock<WorkflowStep>();
            step2.Setup(x => x.Start()).Verifiable();

            var workflow = new Workflow(new[] { step1.Object, step2.Object });

            workflow.StartAndWait();

            step1.Verify(x => x.Start(), Times.Once);
            step2.Verify(x => x.Start(), Times.Once);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void StartAndWait_Discontinues_Workflow_When_Exception_Is_Encountered()
        {
            var step1 = new Mock<WorkflowStep>();
            step1.Setup(x => x.Start()).Throws(new AggregateException());

            var step2 = new Mock<WorkflowStep>();
            step2.Setup(x => x.Start()).Verifiable();


            var workflow = new Workflow(new[] { step1.Object, step2.Object });

            workflow.StartAndWait();

            step1.Verify(x => x.Start(), Times.Once);
            step2.Verify(x => x.Start(), Times.Never);

        }
    }


}
