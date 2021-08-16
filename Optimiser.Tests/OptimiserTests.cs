using System;
using System.Threading.Tasks;
using Optimiser.Core;
using Xunit;

namespace Optimiser.Tests
{
    public class OptimiserTests
    {
        [Fact]
        public void Optimiser_constructor_should_initialise()
        {
            _ = new Optimiser<int>(new TrivialTaskRunner(), new ParameterRange<int>[] { }, new Options());
        }

        [Fact]
        public void Execute_should_return_a_successfull_result_for_trivial_input()
        {
            var optimiser = new Optimiser<int>(new TrivialTaskRunner(), new ParameterRange<int>[] { }, new Options());
            var result = optimiser.Execute();

            Assert.True(result.Success);
            Assert.True(result.ElapsedTimeSpan.Ticks > 0);
        }

        [Fact]
        public void Execute_should_return_()
        {
            var optimiser = new Optimiser<decimal>(new SingleParameterTaskRunner(), new ParameterRange<decimal>[]
            {
                new ParameterRange<decimal>()

            }, new Options());
            var result = optimiser.Execute();

            Assert.True(result.Success);
            Assert.True(result.ElapsedTimeSpan.Ticks > 0);
        }

        [Fact]
        public void Options_WithTimeout_returns_new_options_with_updated_property()
        {
            var options = new Options();

            Assert.Equal(0, options.TimeoutInMs);
            Assert.Equal(10, options.WithTimeoutInMs(10).TimeoutInMs);
        }

        [Fact]
        public void Interval_constructor_from_string_works_for_integers()
        {
            var interval = new Interval<int>("[1,2)");

            Assert.Equal(1, interval.Start.Value);
            Assert.Equal(2, interval.End.Value);
            Assert.True(interval.Start.IsInclusive);
            Assert.False(interval.End.IsInclusive);
        }


        [Fact]
        public void Interval_constructor_from_string_works_for_decimals()
        {
            var interval = new Interval<decimal>("(1.23,2.345]");

            Assert.Equal(1.23m, interval.Start.Value);
            Assert.Equal(2.345m, interval.End.Value);
            Assert.False(interval.Start.IsInclusive);
            Assert.True(interval.End.IsInclusive);
        }

        [Fact]
        public void NumericParameterRange_constructor_from_string_works_for_decimals()
        {
            var npr = new NumericParameterRange<decimal>("[1.0,2.0);(1.23,2.345]");

            var interval = npr.Intervals[0];

            Assert.Equal(1.0m, interval.Start.Value);
            Assert.Equal(2.0m, interval.End.Value);
            Assert.True(interval.Start.IsInclusive);
            Assert.False(interval.End.IsInclusive);

            interval = npr.Intervals[1];

            Assert.Equal(1.23m, interval.Start.Value);
            Assert.Equal(2.345m, interval.End.Value);
            Assert.False(interval.Start.IsInclusive);
            Assert.True(interval.End.IsInclusive);
        }

        class SingleParameterTaskRunner : TaskRunner
        {
            public override void Task()
            {
                throw new NotImplementedException();
            }
        }

        class TrivialTaskRunner : TaskRunner
        {
            public override void Task()
            {
            }
        }
    }
}