using Force.DeepCloner;

namespace Tuna.Core
{
    public class Options
    {
        public Options()
        {
        }

        public int TimeoutInMs { get; internal set; }

        public Options WithTimeoutInMs(int miliseconds)
        {
            var newOptions = this.DeepClone();
            newOptions.TimeoutInMs = miliseconds;

            return  newOptions;
        }
    }
}