namespace Optimiser.Core
{
    public class Result
    {
        public Result(bool Success, int ElapsedTime)
        {
            this.Success = Success;
            this.ElapsedTime = ElapsedTime;
        }

        public bool Success { get; }
        public int ElapsedTime { get; }
    }
}