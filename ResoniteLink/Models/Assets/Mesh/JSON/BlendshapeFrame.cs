using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class BlendshapeFrame
    {
        /// <summary>
        /// Position of the frame within the blendshape animation
        /// When blendshape has only a single frame, this should be set to 1.0
        /// With multiple frames per blendshape, this determines the position at which this set of deltas is fully applied.
        /// </summary>
        [JsonPropertyName("position")]
        public float Position { get; set; }

        /// <summary>
        /// Delta values for vertex positions of this blendshape frame.
        /// Number of deltas MUST match number of vertices
        /// </summary>
        [JsonPropertyName("positionDeltas")]
        public List<float3> PositionDeltas { get; set; }

        /// <summary>
        /// Optional. Delta values for vertex normals of this blendshape frame.
        /// Number of deltas MUST match number of vertices
        /// </summary>
        [JsonPropertyName("normalDeltas")]
        public List<float3> NormalDeltas { get; set; }

        /// <summary>
        /// Optional. Delta values for vertex tangents of this blendshape frame.
        /// Number of deltas MUST match number of vertices
        /// </summary>
        [JsonPropertyName("tangentDeltas")]
        public List<float3> TangentDeltas { get; set; }
    }
}
