
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proximus
{
    public sealed class Workflow
    {

        IEnumerable<WorkflowStep> Steps;
        public Workflow(IEnumerable<WorkflowStep> steps)
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
