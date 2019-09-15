using System.Buffers;
using System.Runtime.InteropServices;

namespace SampleMemoryPool
{
    class Program
    {
        static void Main(string[] args)
        {

            ArrayPool<byte>.Shared.Return(heap);
        }
    }
}
