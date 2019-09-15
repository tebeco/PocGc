using System;
using System.Buffers;

namespace Runtime.Gc
{
    public class Allocator
    {
        private const int DefaultHeapSize = 1024 * 1024 * 5;
        private byte[] _rawHeap = null;
        private Memory<byte> _heap = null;

        public Allocator()
        {
            InitializeHeap(DefaultHeapSize);
        }

        private void InitializeHeap(int heapSize)
        {
            _rawHeap = ArrayPool<byte>.Shared.Rent(heapSize);
            _heap = _rawHeap.AsMemory();
        }

        public Span<byte> Allocate(long size)
        {
            var address = FindSegmentForSize(size);
            return _heap.Slice(address).Span;
        }

        private int FindSegmentForSize(long size)
        {
            return 0;
        }

        internal void Deallocate(long address)
        {

        }
    }
}