using Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp
{
    public class TwoCharString : ReferenceType
    {
        public TwoCharString(char charOne, char charTwo)
            : base(new Dictionary<string, ValueType>
            {
                { "charOne", charOne },
                { "charTwo", charTwo }
            })
        {
        }
    }
}
