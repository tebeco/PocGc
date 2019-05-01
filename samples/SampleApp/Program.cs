using Runtime;
using System;

namespace SampleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var hostBuilder = new RuntimeHostBuilder<Startup>();
            var host = hostBuilder.Build();
            host.Run();
        }
    }
}
