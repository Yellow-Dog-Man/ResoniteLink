using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class BlendshapeRawData
    {
        /// <summary>
        /// Name of the Blendshape
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

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

        /// <summary>
        /// Frames that compose this blendshape
        /// Blendshapes need at least 1 frame
        /// </summary>
        [JsonPropertyName("frames")]
        public List<BlendshapeFrameRawData> Frames { get; set; }

        internal void ComputeBufferOffsets(ImportMeshRawData mesh, ref int offset)
        {
            foreach (var frame in Frames)
                frame.ComputeBufferOffsets(mesh, this, ref offset);
        }

        internal void AssignBuffer(byte[] buffer)
        {
            foreach (var frame in Frames)
                frame.AssignBuffer(buffer);
        }
    }
}
