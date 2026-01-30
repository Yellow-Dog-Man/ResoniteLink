using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        [JsonPropertyName("position")]
        public float2 position { get; set; }
        [JsonPropertyName("size")]
        public float2 size { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IntRect
    {
        [JsonPropertyName("position")]
        public int2 position { get; set; }
        [JsonPropertyName("size")]
        public int2 size { get; set; }
    }
}
