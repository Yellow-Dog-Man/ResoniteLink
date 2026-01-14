using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// A submesh composed of individual triangles.
    /// </summary>
    public class TriangleSubmesh : Submesh
    {
        /// <summary>
        /// All the triangles that form this submesh
        /// </summary>
        [JsonPropertyName("triangles")]
        public List<Triangle> Triangles { get; set; }
    }
}
