using System;
using System.Threading.Tasks;
using Optimiser.Core;
using Xunit;

namespace Optimiser.Tests
{
	public class OptimiserTests
	{
		[Fact]
		public void Runner_constructor_should_initialise()
		{
            _ = new Runner(new TrivialTaskRunner(), Array.Empty<ParameterDomain>(), new Options());
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
		public void Execute_should_return_a_successfull_result_for_trivial_input()
        {
            TrivialTaskRunner taskRunner = new TrivialTaskRunner();
            var runner = new Runner(taskRunner, Array.Empty<ParameterDomain>(), new Options());

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
	}

	class TrivialTaskRunner : AbstractTaskRunner
	{
        public bool IveBeenRun { get; set; } = false;

        public override void Task()
        {            
			IveBeenRun = true;
        }
    }
}
