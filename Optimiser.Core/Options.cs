using Force.DeepCloner;

namespace Optimiser.Core
{
    public class Options
    {
        public Options()
        {
        }

        public int Miliseconds { get; private set; }

        public Options WithTimeoutInMs(int miliseconds)
        {
            var newOptions = this.DeepClone();
            newOptions.Miliseconds = miliseconds;

            return  newOptions;
        }
    }
}