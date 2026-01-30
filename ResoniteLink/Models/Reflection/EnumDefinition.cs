using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class EnumDefinition
    {
        /// <summary>
        /// Structured type information of the enum
        /// </summary>
        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }

        /// <summary>
        /// The backing datatype of this enum. Typically int, but some enums can use byte, short, long and so on.
        /// </summary>
        [JsonPropertyName("backingType")]
        public string BackingType { get; set; }

        /// <summary>
        /// Names of values of this enum and their associated values.
        /// Note that there can be multiple names for the same value.
        /// </summary>
        [JsonPropertyName("values")]
        public Dictionary<string, long> Values { get; set; }

        /// <summary>
        /// Does this enum represent flags?
        /// </summary>
        [JsonPropertyName("isFlags")]
        public bool IsFlags { get; set; }
    }
}
