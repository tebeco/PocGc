using Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp
{
    public class Startup : RuntimeHost
    {
        public override void Run()
        {
            var x = new TwoCharString('c', 'd');
            Console.WriteLine(x.GetValue("charOne"));
            Console.WriteLine(x.GetValue("charTwo"));
        }
    }
}
