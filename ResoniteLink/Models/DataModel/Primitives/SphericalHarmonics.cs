using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

// Representation of spherical harmonics values
// This is a bit more complicated from other types, because spherical harmonics in FrooxEngine are generic types
// This means that the types of the coefficients can be a number of different types - potentially even other
// spherical harmonic types and other generic nested types.
// That's why these are represted as "Data" elements
// Special care should be taken with these - all Data elements MUST be of same type
// The Data elements also MUST be a type that is supported by FrooxEngine - typically any primitives that support multiplication & scaling

namespace ResoniteLink
{
    /// <summary>
    /// First order spherical harmonic
    /// </summary>
    public class SphericalHarmonicsL1
    {
        [JsonPropertyName("sh0")]
        public Data SH0 { get; set; }

        [JsonPropertyName("sh1")]
        public Data SH1 { get; set; }

        [JsonPropertyName("sh2")]
        public Data SH2 { get; set; }

        [JsonPropertyName("sh3")]
        public Data SH3 { get; set; }
    }

    /// <summary>
    /// Second order spherical harmonic
    /// </summary>
    public class SphericalHarmonicsL2
    {
        [JsonPropertyName("sh0")]
        public Data SH0 { get; set; }

        [JsonPropertyName("sh1")]
        public Data SH1 { get; set; }

        [JsonPropertyName("sh2")]
        public Data SH2 { get; set; }

        [JsonPropertyName("sh3")]
        public Data SH3 { get; set; }

        [JsonPropertyName("sh4")]
        public Data SH4 { get; set; }

        [JsonPropertyName("sh5")]
        public Data SH5 { get; set; }

        [JsonPropertyName("sh6")]
        public Data SH6 { get; set; }

        [JsonPropertyName("sh7")]
        public Data SH7 { get; set; }

        [JsonPropertyName("sh8")]
        public Data SH8 { get; set; }
    }

    /// <summary>
    /// Third order spherical harmonic
    /// </summary>
    public class SphericalHarmonicsL3
    {
        [JsonPropertyName("sh0")]
        public Data SH0 { get; set; }

        [JsonPropertyName("sh1")]
        public Data SH1 { get; set; }

        [JsonPropertyName("sh2")]
        public Data SH2 { get; set; }

        [JsonPropertyName("sh3")]
        public Data SH3 { get; set; }

        [JsonPropertyName("sh4")]
        public Data SH4 { get; set; }

        [JsonPropertyName("sh5")]
        public Data SH5 { get; set; }

        [JsonPropertyName("sh6")]
        public Data SH6 { get; set; }

        [JsonPropertyName("sh7")]
        public Data SH7 { get; set; }

        [JsonPropertyName("sh8")]
        public Data SH8 { get; set; }

        [JsonPropertyName("sh9")]
        public Data SH9 { get; set; }

        [JsonPropertyName("sh10")]
        public Data SH10 { get; set; }

        [JsonPropertyName("sh11")]
        public Data SH11 { get; set; }

        [JsonPropertyName("sh12")]
        public Data SH12 { get; set; }

        [JsonPropertyName("sh13")]
        public Data SH13 { get; set; }

        [JsonPropertyName("sh14")]
        public Data SH14 { get; set; }

        [JsonPropertyName("sh15")]
        public Data SH15 { get; set; }
    }


    /// <summary>
    /// Fourth order spherical harmonic
    /// </summary>
    public class SphericalHarmonicsL4
    {
        [JsonPropertyName("sh0")]
        public Data SH0 { get; set; }

        [JsonPropertyName("sh1")]
        public Data SH1 { get; set; }

        [JsonPropertyName("sh2")]
        public Data SH2 { get; set; }

        [JsonPropertyName("sh3")]
        public Data SH3 { get; set; }

        [JsonPropertyName("sh4")]
        public Data SH4 { get; set; }

        [JsonPropertyName("sh5")]
        public Data SH5 { get; set; }

        [JsonPropertyName("sh6")]
        public Data SH6 { get; set; }

        [JsonPropertyName("sh7")]
        public Data SH7 { get; set; }

        [JsonPropertyName("sh8")]
        public Data SH8 { get; set; }

        [JsonPropertyName("sh9")]
        public Data SH9 { get; set; }

        [JsonPropertyName("sh10")]
        public Data SH10 { get; set; }

        [JsonPropertyName("sh11")]
        public Data SH11 { get; set; }

        [JsonPropertyName("sh12")]
        public Data SH12 { get; set; }

        [JsonPropertyName("sh13")]
        public Data SH13 { get; set; }

        [JsonPropertyName("sh14")]
        public Data SH14 { get; set; }

        [JsonPropertyName("sh15")]
        public Data SH15 { get; set; }

        [JsonPropertyName("sh16")]
        public Data SH16 { get; set; }

        [JsonPropertyName("sh17")]
        public Data SH17 { get; set; }

        [JsonPropertyName("sh18")]
        public Data SH18 { get; set; }

        [JsonPropertyName("sh19")]
        public Data SH19 { get; set; }

        [JsonPropertyName("sh20")]
        public Data SH20 { get; set; }

        [JsonPropertyName("sh21")]
        public Data SH21 { get; set; }

        [JsonPropertyName("sh22")]
        public Data SH22 { get; set; }

        [JsonPropertyName("sh23")]
        public Data SH23 { get; set; }

        [JsonPropertyName("sh24")]
        public Data SH24 { get; set; }
    }
}
