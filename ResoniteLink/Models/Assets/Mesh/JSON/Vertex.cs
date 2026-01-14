using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Defines a single vertex of a mesh. Position is mandatory field, but all other properties are optional.
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// Position of the vertex.
        /// </summary>
        [JsonPropertyName("position")]
        public float3 Position { get; set; }

        /// <summary>
        /// Normal vector of the vertex
        /// </summary>
        [JsonPropertyName("normal")]
        public float3? Normal { get; set; }

        /// <summary>
        /// Tangent vector of the vertex. The 4th component indicates direction of the binormal
        /// When specifying tangent, it's strongly recommended that normals are specified too.
        /// </summary>
        [JsonPropertyName("tangent")]
        public float4? Tangent { get; set; }

        /// <summary>
        /// Color of the vertex
        /// </summary>
        [JsonPropertyName("color")]
        public color? Color { get; set; }

        /// <summary>
        /// UV channel coordinates.
        /// Each vertex can have multiple UV channels.
        /// Each UV channel can have 2-4 dimensions.
        /// The number of channels and dimensions for each MUST be same across all vertices.
        /// </summary>
        [JsonPropertyName("uvs")]
        public List<UV_Coordinate> UVs { get; set; }

        /// <summary>
        /// Weights that define how much this vertex is affected by specific bones for skinned meshes.
        /// The weights should add up to 1 across all the weights.
        /// </summary>
        [JsonPropertyName("boneWeights")]
        public List<BoneWeight> BoneWeights { get; set; }
    }
}
