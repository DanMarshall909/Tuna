using System;
using System.Threading.Tasks;
using Tuna.Core;
using Xunit;

namespace Tuna.Tests
{
    public class TunaTests
    {
        [Fact]
        public void Runner_constructor_should_initialise()
        {
            _ = new Runner(new TrivialTaskRunner(), Array.Empty<Parameter>(), new Options());
        }

        [Fact]
        public void Runner_constructor_should_initialise_via_fluid_interface()
        {
            _ = new Runner(new TrivialTaskRunner());
        }

        [Fact]
        public void Runner_constructor_should_initialise_via_fluid_interface_and_add_options()
        {
            var runner = new Runner(new TrivialTaskRunner())
                .WithHardTimeout(10);

            Assert.Equal(10, runner.Options.TimeoutInMs);
        }

        [Fact]
        public void Runner_constructor_should_initialise_via_fluid_interface_and_add_Parameter()
        {
            var runner = new Runner(new TrivialTaskRunner())
                .WithHardTimeout(10)
                .WithParameter<decimal>("Number of rows to insert");

            Assert.True(runner.Parameters[0].Name == "Number of rows to insert");
        }

        [Fact]
        public void Runner_can_add_Parameter_with_intervals()
        {
            var runner = new Runner(new TrivialTaskRunner())
                .WithHardTimeout(10)
                .WithParameter("Number of rows to insert"
                    , new Interval<decimal>("[1,100)")
                    , new Interval<decimal>("(200,500]"))
                .WithParameter<InsertMethod>("Insert method");

            Assert.Equal("Number of rows to insert", runner.Parameters[0].Name);
            Assert.Equal("Insert method", runner.Parameters[1].Name);        
            Assert.Equal(200m, ((NumericParameter<decimal>)runner.Parameters[0]).Intervals[1].Start.Value);
        }

        enum InsertMethod { BulkInsert, IndividualInserts }

        [Fact]
        public void Execute_should_return_a_successfull_result_for_trivial_input()
        {
            TrivialTaskRunner taskRunner = new TrivialTaskRunner();
            var runner = new Runner(taskRunner, Array.Empty<Parameter>(), new Options());

            var result = runner.Run();

            Assert.True(result.Success);
            Assert.True(taskRunner.IveBeenRun);
            Assert.True(result.ElapsedTimeSpan.Ticks > 0);
        }

        [Fact]
        public void Options_WithTimeout_returns_new_options_with_updated_property()
        {
            var options = new Options();

            Assert.Equal(0, options.TimeoutInMs);
            Assert.Equal(10, options.WithTimeoutInMs(10).TimeoutInMs);
        }

        class TrivialTaskRunner : AbstractTaskRunner
        {
            public bool IveBeenRun { get; set; }

            public override void Task() => IveBeenRun = true;
        }
    }
}
