using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tuna.Core;
using Xunit;

namespace Tuna.Tests
{
    public class TunaTests
    {
        [Fact]
        public void Parameters_are_stored_in_correct_order()
        {
            var runner = GetNumericTestRunnerWth2ParametersTimeoutAndIntervalsAnd2Runs(new TrivialTaskRunner());
            Assert.Equal("Number of rows to insert", runner.Parameters[0].Name);
            Assert.Equal("Insert method", runner.Parameters[1].Name);
        }

        [Fact]
        public void Start_value_of_second_interval_is_saved_correctly()
        {
            Runner runner = GetNumericTestRunnerWth2ParametersTimeoutAndIntervalsAnd2Runs(new TrivialTaskRunner());
            Assert.Equal(200m, ((NumericParameter<decimal>)runner.Parameters[0]).Intervals[1].Start.Value);
        }

        [Fact]
        public void Timeout_parameter_is_added_correctly()
        {
            Runner runner = GetNumericTestRunnerWth2ParametersTimeoutAndIntervalsAnd2Runs(new TrivialTaskRunner());
            Assert.Equal(10, runner.Options.Timeout.TotalMilliseconds);
        }

        [Fact]
        public void Task_time_is_recorded()
        {
            Runner runner = GetNumericTestRunnerWth2ParametersTimeoutAndIntervalsAnd2Runs(new TrivialTaskRunner());
            Result result = runner.Runs[0].Result;
            Assert.True(result.ElapsedTimeSpan.Ticks > 0);
        }

        [Fact]
        public void Runs_fail_if_timeout_is_exceeded()
        {
            var runner = new Runner(new TaskRunnerThatExceeds1msPerRun()).WithTimeout(TimeSpan.FromMilliseconds(1));

            runner.Run();

            var result = runner.Runs[0].Result;

            Assert.False(result.Success);
            Assert.Single(result.Errors);
            Assert.Contains("timeout", result.Errors[0].Message, StringComparison.CurrentCultureIgnoreCase);
        }

        [Fact]
        public void Runs_succeed_if_timeout_is_not_exceeded()
        {
            var runner = new Runner(new TrivialTaskRunner()).WithTimeout(TimeSpan.FromMinutes(1));
            runner.Run();

            Assert.All(runner.Runs, r => Assert.True(r.Result.Success));
        }

        [Fact]
        public void Execute_should_capture_exceptions()
        {
            var taskRunner = new ExceptionThrowingTaskRunner();
            var runner = new Runner(taskRunner, new List<Parameter>(), new Options());

            runner.Run();

            var result = runner.Runs[0].Result;
            Assert.False(result.Success);
            Assert.Equal(ExceptionThrowingTaskRunner.ExceptionMessage, result.Errors.First().Message);
        }


        class ExceptionThrowingTaskRunner : AbstractTaskRunner
        {
            public const string ExceptionMessage = "D'oh";

            public override void Task() => throw new Exception(ExceptionMessage);
        }

        static Runner GetNumericTestRunnerWth2ParametersTimeoutAndIntervalsAnd2Runs(TrivialTaskRunner taskRunner)
        {
            var runner = new Runner(taskRunner)
                .WithTimeout(TimeSpan.FromMilliseconds(10))
                .WithParameter("Number of rows to insert"
                    , new Interval<decimal>("[1,100)")
                    , new Interval<decimal>("(200,500]"))
                .WithParameter<TrivialTaskRunner.InsertMethod>("Insert method");

            runner.Run();
            runner.Run();

            return runner;
        }

        class TrivialTaskRunner : AbstractTaskRunner
        {
            public enum InsertMethod { BulkInsert, IndividualInserts }
            public bool IveBeenRun { get; private set; } = false;
            public override void Task() => IveBeenRun = true;
        }
        
        class TaskRunnerThatExceeds1msPerRun : AbstractTaskRunner
        {
            public override void Task()
            {
                Thread.Sleep(1);
            }
        }
    }
}
