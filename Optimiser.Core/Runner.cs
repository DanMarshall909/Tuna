using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimiser.Core
{
    public class Runner
    {
        private readonly TaskRunner taskRunner;
        private readonly ParameterDomain[] parameterDomains;
        private readonly Options options;

        public Runner(TaskRunner taskRunner, ParameterDomain[] parameterDomains, Options options)
        {
            this.taskRunner = taskRunner;
            this.parameterDomains = parameterDomains;
            this.options = options;
        }
        public Result Execute()
        {
            taskRunner.Execute();

            return taskRunner.Result;
        }
    }
}
