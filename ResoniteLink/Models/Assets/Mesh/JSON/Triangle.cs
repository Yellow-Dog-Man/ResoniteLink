using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Represents a single triangle of a mesh
    /// </summary>
    public class Triangle
    {
        /// <summary>
        /// Index of the first vertex that forms this triangle
        /// </summary>
        [JsonPropertyName("vertex0Index")]
        public int Vertex0Index { get; set; }

        /// <summary>
        /// Index of the second vertex that forms this triangle
        /// </summary>
        [JsonPropertyName("vertex1Index")]
        public int Vertex1Index { get; set; }

        /// <summary>
        /// Index of the third vertex that forms this triangle
        /// </summary>
        [JsonPropertyName("vertex2Index")]
        public int Vertex2Index { get; set; }
    }
}
