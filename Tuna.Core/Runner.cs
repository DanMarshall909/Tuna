using System;
using System.Collections.Generic;
using System.Linq;
using Force.DeepCloner;

namespace Tuna.Core
{
    public class Runner
    {
        internal readonly AbstractTaskRunner taskRunner;

        public Runner(AbstractTaskRunner taskRunner)
            : this(taskRunner, new List<Parameter>(), new Options()) { }


        public Runner(AbstractTaskRunner taskRunner, List<Parameter> parameterDomains, Options options)
        {
            this.taskRunner = taskRunner;
            Parameters = parameterDomains;
            Options = options;
        }

        public Options Options { get; internal set; }

        public List<Parameter> Parameters { get; internal set; }

        public List<Run> Runs { get;  set; } = new List<Run>();

        public void Run()
        {
            taskRunner.RunTask();

            Runs.Add(new Run(taskRunner.Result));
        }
    }

    public class Run
    {
        public Run(Result result)
        {
            Result = result;
        }

        public Result Result { get; }
    }

    public static class RunnerExtensionMethods
    {
        public static Runner WithHardTimeout(this Runner runner, int timeoutInMs)
        {
            var newRunner = runner.DeepClone();
            newRunner.Options.TimeoutInMs = timeoutInMs;

            return newRunner;
        }

        public static Runner WithParameter<T>(this Runner runner, string name, List<Interval<T>> intervals) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            var newRunner = runner.DeepClone();
            newRunner.Parameters.Add(new NumericParameter<T>(name, intervals));

            return newRunner;
        }

        public static Runner WithParameter<T>(this Runner runner, string name, params Interval<T>[] intervals) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
        {
            var newRunner = runner.DeepClone();
            newRunner.Parameters.Add(new NumericParameter<T>(name, intervals.ToList()));

            return newRunner;
        }

        public static Runner WithParameter<T>(this Runner runner, string name) where T : Enum
        {
            var newRunner = runner.DeepClone();
            newRunner.Parameters.Add(new EnumParameter<T>(name));

            return newRunner;
        }
    }
}
