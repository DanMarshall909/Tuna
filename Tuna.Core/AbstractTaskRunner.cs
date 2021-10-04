using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Tuna.Core.Errors;

namespace Tuna.Core
{
    public abstract class AbstractTaskRunner
    {
        public Result Result { get; private set; }
        public List<ITunaError> Errors { get; private set; } = new List<ITunaError>();
        public bool IsSuccessful => !Errors.Any();

        public abstract void Task();

        public void RunTask(Runner runner)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                Task();

                if (runner.Options.IsTimeoutExceeded(sw.Elapsed))
                    Errors.Add(new TunaTimeoutError(sw.Elapsed, runner.Options.Timeout));

            }
            catch (Exception exception)
            {
                Errors.Add(new TunaExceptionError(exception));
            }
            finally
            {
                sw.Stop();
                
                Result = new Result(IsSuccessful, new TimeSpan(sw.ElapsedTicks), Errors);
            }

        }
    }
}