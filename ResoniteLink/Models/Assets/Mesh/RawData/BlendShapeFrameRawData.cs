using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class BlendShapeFrameRawData
    {
        /// <summary>
        /// Position of the frame within the blendshape animation
        /// When blendshape has only a single frame, this should be set to 1.0
        /// With multiple frames per blendshape, this determines the position at which this set of deltas is fully applied.
        /// </summary>
        [JsonPropertyName("position")]
        public float Position { get; set; }

        /// <summary>
        /// Indicates if this blendshape frame has normal deltas
        /// </summary>
        [JsonPropertyName("hasNormalDeltas")]
        public bool HasNormalDeltas { get; set; }

        /// <summary>
        /// Indicates if this blendshape frame has tangent deltas
        /// </summary>
        [JsonPropertyName("hasTangentDeltas")]
        public bool HasTangentDeltas { get; set; }

        [JsonIgnore]
        public Span<float3> PositionDeltas => _positionDeltas.Access(buffer);

        [JsonIgnore]
        public Span<float3> NormalDeltas => _normalDeltas.Access(buffer);

        [JsonIgnore]
        public Span<float3> TangentDeltas => _tangentDeltas.Access(buffer);

        byte[] buffer;

        BufferSegment<float3> _positionDeltas;
        BufferSegment<float3> _normalDeltas;
        BufferSegment<float3> _tangentDeltas;

        internal void ComputeBufferOffsets(ImportMeshRawData mesh, ref int offset)
        {
            _positionDeltas = BufferSegment<float3>.AllocateBuffer(mesh.VertexCount, ref offset);
            _normalDeltas = BufferSegment<float3>.AllocateBuffer(HasNormalDeltas ? mesh.VertexCount : 0, ref offset);
            _tangentDeltas = BufferSegment<float3>.AllocateBuffer(HasTangentDeltas ? mesh.VertexCount : 0, ref offset);
        }

        internal void AssignBuffer(byte[] buffer)
        {
            this.buffer = buffer;
        }
    }
}
