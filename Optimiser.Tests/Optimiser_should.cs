using System;
using Optimiser.Core;
using Xunit;

namespace Optimiser.Tests
{
	public class Optimiser_should
	{
		[Fact]
		public void Initialise()
		{
			var parameters = new Core.Parameter [] { };
            _ = new Core.Optimiser(parameters);
        }
	}
}
