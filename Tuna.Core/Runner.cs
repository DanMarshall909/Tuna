using System;
using System.Collections.Generic;
using System.Linq;
using Force.DeepCloner;

namespace Tuna.Core
{
    public class Runner
    {
        public Runner(AbstractTaskRunner taskRunner)
            : this(taskRunner, new List<Parameter>(), new Options()) { }


        public Runner(AbstractTaskRunner taskRunner, IList<Parameter> parameterDomains, Options options)
        {
            TaskRunner = taskRunner;
            Parameters = parameterDomains;
            Options = options;
        }

        public Options Options { get; internal set; }

        public IList<Parameter> Parameters { get; internal set; }

        public Dictionary<string, Parameter> ParametersByName => Parameters.ToDictionary(x => x.Name, x => x);

        public IList<Run> Runs { get; set; } = new List<Run>();

        public AbstractTaskRunner TaskRunner { get; }

        public void Run()
        {
            TaskRunner.RunTask(this);

            Runs.Add(new Run(TaskRunner.Result));
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
        public static Runner WithTimeout(this Runner runner, TimeSpan timeout)
        {
            var newRunner = runner.DeepClone();
            newRunner.Options.Timeout = timeout;

            return newRunner;
        }

        public static Runner WithParameter<T>(this Runner runner, string name, IList<Interval<T>> intervals) where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
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
