using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// A submesh composed of individual triangles.
    /// This is an alternate representation and will result in same submesh as TriangleSubmesh
    /// With this representation you must take care to provide the indicies for each triangle properly.
    /// Each triangle requires three indicies. Those indicies are consecutive.
    /// </summary>
    public class TriangleSubmeshFlat : Submesh
    {
        /// <summary>
        /// Indexes of vertices representing triangles of this mesh.
        /// Note that each triangle needs three consecutive indicies in this list.
        /// </summary>
        [JsonPropertyName("vertexIndicies")]
        public List<int> VertexIndicies { get; set; }
    }
}
