using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimiser.Core
{
    public class Runner
    {
        internal readonly AbstractTaskRunner taskRunner;
        internal readonly ParameterDomain[] parameterDomains;

        public Runner(AbstractTaskRunner taskRunner)
            : this(taskRunner, Array.Empty<ParameterDomain>(), new Options()) { }


        public Runner(AbstractTaskRunner taskRunner, ParameterDomain[] parameterDomains, Options options)
        {
            this.taskRunner = taskRunner;
            this.parameterDomains = parameterDomains;
            this.Options = options;
        }

        public Options Options { get; internal set; }

        public Result Run()
        {
            taskRunner.Run();

            return taskRunner.Result;
        }
    }

    
    public static class RunnerExtensionMethods
    {
        public static Runner WithHardTimeout(this Runner runner, int timeoutInMs)
        {
            runner.Options.TimeoutInMs = timeoutInMs;

            return runner;
        }
    }
}
