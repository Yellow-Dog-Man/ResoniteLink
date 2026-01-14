using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Maps vertex to a specific bone with specific height
    /// </summary>
    public struct BoneWeight
    {
        /// <summary>
        /// Index of the bone this maps too in the Bones list of the mesh
        /// </summary>
        [JsonPropertyName("boneIndex")]
        public int BoneIndex { get; set; }

        /// <summary>
        /// Weight from 0...1 that influences how much is this vertex affected by the bone.
        /// </summary>
        [JsonPropertyName("weight")]
        public float Weight { get; set; }
    }
}
