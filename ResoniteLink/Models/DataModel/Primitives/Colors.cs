using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    // The struct layout is not necessary for serialization itself, but this simplifies the use
    // of these types within FrooxEngine itself
    [StructLayout(LayoutKind.Sequential)]
    public struct color
    {
        [JsonPropertyName("r")]
        public float r { get; set; }

        [JsonPropertyName("g")]
        public float g { get; set; }

        [JsonPropertyName("b")]
        public float b { get; set; }

        [JsonPropertyName("a")]
        public float a { get; set; }
    }

    // The struct layout is not necessary for serialization itself, but this simplifies the use
    // of these types within FrooxEngine itself
    [StructLayout(LayoutKind.Sequential)]
    public struct colorX
    {
        [JsonPropertyName("r")]
        public float r { get; set; }

        [JsonPropertyName("g")]
        public float g { get; set; }

        [JsonPropertyName("b")]
        public float b { get; set; }

        [JsonPropertyName("a")]
        public float a { get; set; }

        [JsonPropertyName("profile")]
        public string Profile { get; set; }
    }

    // The struct layout is not necessary for serialization itself, but this simplifies the use
    // of these types within FrooxEngine itself
    [StructLayout(LayoutKind.Sequential)]
    public struct color32
    {
        [JsonPropertyName("r")]
        public byte r { get; set; }

        [JsonPropertyName("g")]
        public byte g { get; set; }

        [JsonPropertyName("b")]
        public byte b { get; set; }

        [JsonPropertyName("a")]
        public byte a { get; set; }
    }
}