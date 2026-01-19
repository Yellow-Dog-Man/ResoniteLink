using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class TypeDefinition
    {
        /// <summary>
        /// The full type encoded using Resonite's type encoding (which is similar to C# type definitions)
        /// Useful for matching the exact types and when providing the type to Resonite when instantiating
        /// </summary>
        [JsonPropertyName("fullTypeName")]
        public string FullTypeName { get; set; }

        /// <summary>
        /// Name of the assembly that this type is contained in. This is only filled for Resonite data model types.
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// Full namespace path where this type is defined
        /// </summary>
        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Name of the type itself, without namespace
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// For generic types, this lists all the generic arguments for this type.
        /// </summary>
        [JsonPropertyName("genericArguments")]
        public List<GenericArgumentDefinition> GenericArguments { get; set; }
    }
}
