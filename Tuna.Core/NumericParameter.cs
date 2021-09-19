using System;
using System.Collections.Generic;

namespace Tuna.Core
{

    public class NumericParameter<T> : Parameter where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>
    {
        public List<Interval<T>> Intervals { get; private set; }

        public NumericParameter(string name, List<Interval<T>> intervals)
        {
            Name = name;
            Intervals = intervals;
        }
        public override object CurrentValue => throw new NotImplementedException();
    }
}