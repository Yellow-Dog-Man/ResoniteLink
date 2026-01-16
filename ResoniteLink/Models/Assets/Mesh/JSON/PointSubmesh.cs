using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class PointSubmesh : Submesh
    {
        /// <summary>
        /// Indexes of vertices for each point in this submesh.
        /// </summary>
        [JsonPropertyName("vertexIndices")]
        public List<int> VertexIndices { get; set; }
    }
}
