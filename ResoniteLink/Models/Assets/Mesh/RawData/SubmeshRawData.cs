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
        protected abstract int IndicieCount { get; }

        [JsonIgnore]
        public Span<int> Indicies => _indicies.Access(buffer);

        byte[] buffer;
        BufferSegment<int> _indicies;

        internal void ComputeBufferOffsets(ref int offset)
        {
            _indicies = BufferSegment<int>.AllocateBuffer(IndicieCount, ref offset);
        }

        internal void AssignBuffer(byte[] buffer)
        {
            this.buffer = buffer;
        }
    }
}
