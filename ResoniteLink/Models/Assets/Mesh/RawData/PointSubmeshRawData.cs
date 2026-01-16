using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class PointSubmeshRawData : SubmeshRawData
    {
        /// <summary>
        /// How many points are in this submesh
        /// </summary>
        [JsonPropertyName("pointCount")]
        public int PointCount { get; set; }

        protected override int IndexCount => PointCount;
    }
}
