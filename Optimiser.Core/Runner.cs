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
            : this(taskRunner, Array.Empty<Parameter>(), new Options()) { }


        public Runner(AbstractTaskRunner taskRunner, Parameter[] parameterDomains, Options options)
        {
            this.taskRunner = taskRunner;
            Parameters = parameterDomains;
            Options = options;
        }

        public Options Options { get; internal set; }

        public Parameter[] Parameters { get; internal set; }

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
        public static Runner WithParameter<T>(this Runner runner, string name, params Interval<T>[] intervals) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            var newRunner = runner.DeepClone();            
            newRunner.Parameters = runner
                .Parameters
                .Append(new NumericParameter<T>(name, intervals))
                .ToArray();
            
            return newRunner;
        }

        
        public static Runner WithParameter<T>(this Runner runner, string name) where T : Enum      
        {
            var newRunner = runner.DeepClone();            
            newRunner.Parameters = runner
                .Parameters
                .Append(new EnumParameter<T>(name))
                .ToArray();
            
            return newRunner;
        }
    }
}
