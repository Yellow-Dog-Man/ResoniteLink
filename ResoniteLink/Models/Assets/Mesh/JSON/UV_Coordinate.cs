using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    [JsonDerivedType(typeof(UV2D_Coordinate), "2D")]
    [JsonDerivedType(typeof(UV3D_Coordinate), "3D")]
    [JsonDerivedType(typeof(UV4D_Coordinate), "4D")]
    public abstract class UV_Coordinate
    {

    }

    public class UV2D_Coordinate : UV_Coordinate
    {
        [JsonPropertyName("uv")]
        public float2 uv { get; set; }
    }

    public class UV3D_Coordinate : UV_Coordinate
    {
        [JsonPropertyName("uv")]
        public float3 uv { get; set; }
    }

    public class UV4D_Coordinate : UV_Coordinate
    {
        [JsonPropertyName("uv")]
        public float4 uv { get; set; }
    }
}
