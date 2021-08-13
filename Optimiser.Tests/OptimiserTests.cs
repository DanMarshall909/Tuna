using System;
using Optimiser.Core;
using Xunit;

namespace Optimiser.Tests
{
	public class OptimiserTests
	{
		[Fact]
		public void Optimiser_constructor_should_initialise()
		{
			var parameters = new ParameterRange[] { };

			ITaskRunner taskRunner = new TrivialTask();

			Options options = new Options();

            _ = new Core.Optimiser(taskRunner, parameters, options);
        }

		[Fact]
		public void Execute_should_return_a_successfull_result_for_trivial_input()
        {
			var parameters = new ParameterRange[] { };
			var options = new Options();

        }
	}

	class TrivialTask : ITaskRunner
	{
        public Result Execute()
        {
			return new Result(Success: true, ElapsedTime: 10);
		}
    }
}
