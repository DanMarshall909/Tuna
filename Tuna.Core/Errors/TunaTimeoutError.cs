using System;

namespace Tuna.Core.Errors
{
    internal class TunaTimeoutError : ITunaError
    {
        public TunaTimeoutError(TimeSpan elapsed, TimeSpan timeout)
        {
            Elapsed = elapsed;
            Timeout = timeout;
        }

        public string Message => $"Task completed in {Elapsed}. Timeout of {Timeout} exceeded.";

        public TimeSpan Elapsed { get; }
        public TimeSpan Timeout { get; }
    }
}