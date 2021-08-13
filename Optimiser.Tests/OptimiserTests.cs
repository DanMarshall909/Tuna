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
            _ = new Core.Optimiser(new TrivialTaskRunner(), new ParameterRange[] { }, new Options());
        }

		[Fact]
		public void Execute_should_return_a_successfull_result_for_trivial_input()
        {
			var optimiser = new Core.Optimiser(new TrivialTaskRunner(), new ParameterRange[] { }, new Options());

			var result = optimiser.Execute();

			Assert.Equal(true, result.Success);
			Assert.True(result.ElapsedTimeSpan.Ticks > 0);
		}

		[Fact]
		public void Options_WithTimeout_returns_new_options_with_updated_property()
        {
			var options = new Options();

			Assert.Equal(0, options.TimeoutInMs);
			Assert.Equal(10, options.WithTimeoutInMs(10).TimeoutInMs);
        }
	}

	class TrivialTaskRunner : TaskRunner
	{
        public override void Task()
        {            
        }
    }
}
