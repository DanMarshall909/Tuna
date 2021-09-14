using System;

namespace Optimiser.Core
{
    public abstract class Parameter
    {
        public string Name { get; protected set; }
        public abstract object CurrentValue { get; }
    }

    public class NumericParameter<T> : Parameter where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
    {
        public Interval<T>[] Intervals { get; private set; }

        public NumericParameter(string name, Interval<T>[] intervals)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Intervals = intervals ?? throw new ArgumentNullException(nameof(intervals));
        }
        public override object CurrentValue => throw new NotImplementedException();
    }
}