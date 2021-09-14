using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimiser.Core
{
    public class Runner
    {
        private readonly AbstractTaskRunner taskRunner;
        private readonly ParameterDomain[] parameterDomains;
        private readonly Options options;

        public Runner(AbstractTaskRunner taskRunner) 
            : this(taskRunner, Array.Empty<ParameterDomain>(), new Options()) { }
        

        public Runner(AbstractTaskRunner taskRunner, ParameterDomain[] parameterDomains, Options options)
        {
            this.taskRunner = taskRunner;
            this.parameterDomains = parameterDomains;
            this.options = options;
        }
        public Result Run()
        {
            taskRunner.Run();

            return taskRunner.Result;
        }
    }
}
