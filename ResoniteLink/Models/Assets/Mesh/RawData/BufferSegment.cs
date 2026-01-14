using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ResoniteLink
{
    internal readonly struct BufferSegment<T>
        where T : unmanaged
    {
        public readonly int byteStart;
        public readonly int byteLength;

        public BufferSegment(int byteStart, int byteLength)
        {
            this.byteStart = byteStart;
            this.byteLength = byteLength;
        }

        public Span<T> Access(byte[] buffer) => MemoryMarshal.Cast<byte, T>(buffer.AsSpan(byteStart, byteLength));

        internal static unsafe BufferSegment<T> AllocateBuffer(int count, ref int offset)
        {
            var bytes = sizeof(T) * count;

            var segment = new BufferSegment<T>(offset, bytes);

            offset += bytes;

            return segment;
        }
    }
}
