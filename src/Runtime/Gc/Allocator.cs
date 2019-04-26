using System;

namespace Gc.Core
{
    public class Allocator
    {
        public Span<byte> Allocate(long size)
        {
            return Span<byte>.Empty;
        }

        internal void Deallocate(long address)
        {

        }
    }
}