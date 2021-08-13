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
            _ = new Core.Optimiser();
        }
	}
}
