using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class GenericParameter
    {
        /// <summary>
        /// Name of the generic parameter. This is used to match up
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// If this parameter constrains a generic type, this will contain the full type name of the constraint.
        /// </summary>
        [JsonPropertyName("typeName")]
        public string TypeName { get; set; }

        /// <summary>
        /// Requires that this parameter is an unmanaged type
        /// See C# documentation for the unmanaged keyword
        /// </summary>
        [JsonPropertyName("unmanaged")]
        public bool Unmanaged { get; set; }

        /// <summary>
        /// Requires that this parameter is a struct type
        /// See C# documentation for the struct keyword
        /// </summary>
        [JsonPropertyName("struct")]
        public bool Struct { get; set; }

        /// <summary>
        /// Requires that this parameter is a class type
        /// See C# documentation for the struct keyword
        /// </summary>
        [JsonPropertyName("class")]
        public bool Class { get; set; }
    }
}
