using System;
using System.Collections.Generic;
using Tuna.Core;
using Xunit;

namespace Tuna.Tests
{
    public class TunaTests
    {
		[Fact]
        public void Parameters_are_stored_in_correct_order()
		{
			Runner runner = GetNumericTestRunnerWth2ParametersHardTimeoutAndIntervals(new TrivialTaskRunner());
            Assert.Equal("Number of rows to insert", runner.Parameters[0].Name);
            Assert.Equal("Insert method", runner.Parameters[1].Name);
		}
		
		[Fact]
        public void Start_value_of_second_interval_is_saved_correctly()
		{
			Runner runner = GetNumericTestRunnerWth2ParametersHardTimeoutAndIntervals(new TrivialTaskRunner());            
            Assert.Equal(200m, ((NumericParameter<decimal>)runner.Parameters[0]).Intervals[1].Start.Value);
		}
		
		[Fact]
        public void Timeout_parameter_is_added_correctly()
		{
			Runner runner = GetNumericTestRunnerWth2ParametersHardTimeoutAndIntervals(new TrivialTaskRunner());
            Assert.Equal(10, runner.Options.TimeoutInMs);
		}
		
		[Fact]
        public void Task_time_is_recorded()
		{
			Runner runner = GetNumericTestRunnerWth2ParametersHardTimeoutAndIntervals(new TrivialTaskRunner());            
            Result result = runner.Runs[0].Result;
            Assert.True(result.ElapsedTimeSpan.Ticks > 0);
		}
		
		[Fact]
        public void Runs_fail_if_timeout_is_exceeded()
		{
			var runner = GetNumericTestRunnerWth2ParametersHardTimeoutAndIntervals(new TrivialTaskRunner());
            
            runner.Run();
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
            Assert.Equal(ExceptionThrowingTaskRunner.ExceptionMessage, result.Exception.Message);
        }

        [Fact]
        public void Options_WithTimeout_returns_new_options_with_updated_property()
        {
            var options = new Options();

            Assert.Equal(0, options.TimeoutInMs);
            Assert.Equal(10, options.WithTimeoutInMs(10).TimeoutInMs);
        }
        
        class ExceptionThrowingTaskRunner : AbstractTaskRunner
        {
            public const string ExceptionMessage = "D'oh";
        
            public override void Task() => throw new Exception(ExceptionMessage);
        }
        
        static Runner GetNumericTestRunnerWth2ParametersHardTimeoutAndIntervals(TrivialTaskRunner taskRunner)
        {
            var runner = new Runner(taskRunner)
                .WithHardTimeout(10)
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


    }
}
