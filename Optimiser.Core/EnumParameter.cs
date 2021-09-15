using System;

namespace Optimiser.Core
{
    public class EnumParameter<T> : Parameter where T : Enum
    {
        public EnumParameter(string name)
        {
            Name = name;
        }

        public override object CurrentValue => throw new NotImplementedException();
    }
}