using System;
using System.Collections.Generic;
using Tuna.Core.Errors;

namespace Tuna.Core
{
    public class Result
    {
        public Result(bool success, TimeSpan elapsedTimeSpan, List<ITunaError> errors = null)
        {
            Success = success;
            ElapsedTimeSpan = elapsedTimeSpan;
            Errors = errors ?? new List<ITunaError>();
        }

        public bool Success { get; } = false;
        public TimeSpan ElapsedTimeSpan { get; }
        public List<ITunaError> Errors { get; }
    }
}