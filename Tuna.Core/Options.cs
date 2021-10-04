using Force.DeepCloner;
using System;

namespace Tuna.Core
{
    public class Options
    {
        private static readonly TimeSpan UnsetTimeout = new(0);

        public TimeSpan Timeout { get; internal set; } = UnsetTimeout;

        public Options WithTimeout(TimeSpan timeout)
        {
            var newOptions = this.DeepClone();
            newOptions.Timeout = timeout;

            return newOptions;
        }

        internal bool IsTimeoutExceeded(TimeSpan timeSpan) => Timeout != UnsetTimeout && timeSpan > Timeout;
    }
}