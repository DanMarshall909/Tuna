using System;

namespace Tuna.Core.Errors
{
    internal class TunaExceptionError : ITunaError
    {

        public TunaExceptionError(Exception exception)
        {
            Exception = exception;
        }

        public string Message => Exception.Message;

        public Exception Exception { get; }
    }
}