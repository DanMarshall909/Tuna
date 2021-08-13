using System;
using System.Diagnostics;

namespace Optimiser.Core
{
    public abstract class TaskRunner
    {
        public Result Result { get; private set; }

        public abstract void Task();

        public void Execute()
        {
            var success = false;
            Exception exception = null;            

            var sw = Stopwatch.StartNew();
            try
            {
                Task();
                success = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                sw.Stop();

                Result = new Result(success, new TimeSpan(sw.ElapsedTicks), exception);
            }
        }
    }
}