using System;

namespace Tuna.Core
{
    public class Result
    {
        public Result(bool success, TimeSpan elapsedTimeSpan, Exception exception = null)
        {
            Success = success;
            ElapsedTimeSpan = elapsedTimeSpan;
            Exception = exception;
        }

        public bool Success { get; }
        public TimeSpan ElapsedTimeSpan { get; }
        public Exception Exception { get; }
    }
}