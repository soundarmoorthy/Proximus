
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proximus
{
    internal sealed class Workflow
    {

        private IEnumerable<WorkflowStep> Steps;
        internal Workflow(IEnumerable<WorkflowStep> steps)
        {
            this.Steps = steps;
        }

        public void StartAndWait()
        {
            var task = Start();
            task.Wait();
        }

        public async Task Start()
        {
           await  Task.Factory.StartNew(() =>
           {    

               foreach (var step in Steps)
               {
                   step.Start();
               }
           });
        }
    }
}
