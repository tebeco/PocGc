using System;
using System.Collections.Generic;
using System.Text;

namespace Runtime
{
    public abstract class RuntimeHost : ReferenceType
    {
        public RuntimeHost()
        {

        }

        public abstract void Run();
    }
}
