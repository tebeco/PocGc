using System;

namespace Gc.Core
{
    public class Mutator
    {
        private readonly Allocator _allocator;

        public Mutator(Allocator allocator)
        {
            _allocator = allocator;
        }

        public Span<byte> Allocate(long size)
        {
            return _allocator.Allocate(size);
        }

        public Span<byte> ReadOnlyMemory(long address)
        {
            return Span<byte>.Empty;
        }
    
        public void Write(int address, Span<byte> value)
        {

        }
    }
}