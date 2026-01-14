using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class TriangleSubmeshRawData : SubmeshRawData
    {
        /// <summary>
        /// How many triangles are in this submesh
        /// </summary>
        [JsonPropertyName("triangleCount")]
        public int TriangleCount { get; set; }

        protected override int IndicieCount => TriangleCount * 3;
    }
}
