using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Imports a mesh asset from purely JSON definition.
    /// This is pretty verbose, so it's recommended only for smaller meshes, but is supported for
    /// convenience and ease of implementation & experimentation, at the cost of efficiency.
    /// If possible, it's recommended to use ImportMeshRawData for better efficiency.
    /// </summary>
    public class ImportMeshJSON : Message
    {
        /// <summary>
        /// Vertices of this mesh. These are shared across sub-meshes
        /// </summary>
        [JsonPropertyName("vertices")]
        public List<Vertex> Vertices { get; set; }

        /// <summary>
        /// List of submeshes (points, triangles...) representing this mesh.
        /// Meshes will typically have at least one submesh.
        /// Each submesh uses indicies of the vertices for its primitives.
        /// </summary>
        [JsonPropertyName("submeshes")]
        public List<Submesh> Submeshes { get; set; }

        /// <summary>
        /// Bones of the mesh when data represents a skinned mesh.
        /// These will be referred to by their index from vertex data.
        /// </summary>
        [JsonPropertyName("bones")]
        public List<Bone> Bones { get; set; }

        /// <summary>
        /// Blendshapes of this mesh.
        /// These allow modifying the vertex positions, normals & tangents for animations such as facial expressions.
        /// </summary>
        [JsonPropertyName("blendshapes")]
        public List<BlendShape> BlendShapes { get; set; }
    }
}
