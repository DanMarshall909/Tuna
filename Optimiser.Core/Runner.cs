using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Force.DeepCloner;

namespace Optimiser.Core
{
    public class Runner
    {
        internal readonly AbstractTaskRunner taskRunner;

        public Runner(AbstractTaskRunner taskRunner)
            : this(taskRunner, Array.Empty<ParameterDomain>(), new Options()) { }


        public Runner(AbstractTaskRunner taskRunner, ParameterDomain[] parameterDomains, Options options)
        {
            this.taskRunner = taskRunner;
            this.ParameterDomains = parameterDomains;
            this.Options = options;
        }

        public Options Options { get; internal set; }

        public ParameterDomain[] ParameterDomains { get; internal set; }

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
            var newRunner = runner.DeepClone();            
            newRunner.Options.TimeoutInMs = timeoutInMs;

            return newRunner;
        }
        public static Runner WithParameter<T>(this Runner runner, string name)
        {
            var newRunner = runner.DeepClone();            
            newRunner.ParameterDomains = runner
                .ParameterDomains
                .Append(new ParameterDomain(name))
                .ToArray();
            
            return newRunner;
        }
    }
}
