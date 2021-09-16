namespace Tuna.Core
{
    public abstract class Parameter
    {
        public string Name { get; protected set; }
        public abstract object CurrentValue { get; }
    }
}