using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimiser.Core
{
    public class Optimiser
    {
        private readonly ITaskRunner taskRunner;
        private readonly ParameterRange[] parameters;
        private readonly Options options;

        public Optimiser(ITaskRunner taskRunner, ParameterRange[] parameters, Options options)
        {
            this.taskRunner = taskRunner;
            this.parameters = parameters;
            this.options = options;
        }
        public Result Execute()
        {
            return taskRunner.Execute();
        }
    }
}
