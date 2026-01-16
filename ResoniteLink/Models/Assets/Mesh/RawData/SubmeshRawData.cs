using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    [JsonDerivedType(typeof(PointSubmeshRawData), "points")]
    [JsonDerivedType(typeof(TriangleSubmeshRawData), "triangles")]
    public abstract class SubmeshRawData
    {
        protected abstract int IndexCount { get; }

        [JsonIgnore]
        public Span<int> Indices => _indices.Access(buffer);

        byte[] buffer;
        BufferSegment<int> _indices;

        internal void ComputeBufferOffsets(ref int offset)
        {
            _indices = BufferSegment<int>.AllocateBuffer(IndexCount, ref offset);
        }

        internal void AssignBuffer(byte[] buffer)
        {
            this.buffer = buffer;
        }
    }
}
