using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimiser.Core
{
    public class Optimiser
    {
        private Parameter[] parameters;
        private Optimiser() { }
        public Optimiser(Parameter[] parameters)
        {
            this.parameters = parameters;
        }
    }
}
