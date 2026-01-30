using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Represents a reference to another type - typically base type or an interface
    /// </summary>
    public class TypeReference
    {
        /// <summary>
        /// The typename of the referenced type. For generic types, this will always be generic type definition.
        /// For generic types the arguments are specified separately.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// For generic referenced types, this is list of generic arguments used.
        /// These can be either actual types - or they can be the generic parameters of the derived class.
        /// Make sure to check if the name of the generic parameter matches the generic parameters first before trying to get the type definition!
        /// </summary>
        [JsonPropertyName("genericArguments")]
        public List<string> GenericArguments { get; set; }
    }
}
