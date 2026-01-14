using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Represents a bone of a mesh
    /// </summary>
    public class Bone
    {
        /// <summary>
        /// Name of the bone.
        /// This generally doesn't have much actual function for mesh data, but is useful for references and debugging.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The bind pose of the bone - its default transform in model space.
        /// This is essentially the pose of the bone relative to the vertices where the vertices bound to it will be in their original spot. 
        /// </summary>
        [JsonPropertyName("bindPose")]
        public float4x4 BindPose { get; set; }
    }
}
