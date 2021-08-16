using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimiser.Core
{
    public class Optimiser<T>
    {
        private readonly TaskRunner taskRunner;
        private readonly ParameterRange<T>[] parameters;
        private readonly Options options;

        public Optimiser(TaskRunner taskRunner, ParameterRange<T>[] parameters, Options options)
        {
            this.taskRunner = taskRunner;
            this.parameters = parameters;
            this.options = options;
        }
        public Result Execute()
        {
            taskRunner.Execute();

            return taskRunner.Result;
        }
    }
}
