using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Provides a structured type definition. It includes the FullTypeName, which is the type as Resonite will accept it.
    /// It also additional provides decomposed information about the type - this can be used by some implementations to generate
    /// structured bindings and do some type validations and instantiation.
    /// If you do not care about this information, you can just use FullTypeName. However this will not be sufficient for
    /// generic types, as those types require generic arguments.
    /// </summary>
    public class TypeDefinition
    {
        /// <summary>
        /// Contains definition of base type of this type if requested and if the base type is relevant to ResoniteLink.
        /// If definitions of components are requested as flattened, base types are omitted.
        /// </summary>
        [JsonPropertyName("baseType")]
        public string BaseType { get; set; }

        /// <summary>
        /// The full type encoded using Resonite's type encoding (which is similar to C# type definitions)
        /// Useful for matching the exact types and when providing the type to Resonite when instantiating
        /// </summary>
        [JsonPropertyName("fullTypeName")]
        public string FullTypeName { get; set; }

        /// <summary>
        /// Name of the assembly that this type is contained in. This is only filled for Resonite data model types.
        /// </summary>
        [JsonPropertyName("assemblyName")]
        public string AssemblyName { get; set; }

        /// <summary>
        /// Full namespace path where this type is defined
        /// </summary>
        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }

        /// <summary>
        /// Name of the type itself, without namespace or generic arguments
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Indicates if this type is an interface
        /// </summary>
        [JsonPropertyName("isInterface")]
        public bool IsInterface { get; set; }

        /// <summary>
        /// Indicates if this datatype is an engine primitive - one that can be used as value in fields
        /// </summary>
        [JsonPropertyName("isEnginePrimitive")]
        public bool IsEnginePrimitive { get; set; }

        /// <summary>
        /// Indicates if this type represents a generic parameter - a placeholder for generic types, which can be
        /// replaced with other types.
        /// </summary>
        [JsonPropertyName("isGenericParameter")]
        public bool IsGenericParameter { get; set; }

        /// <summary>
        /// For generic types, this lists all the generic arguments for this type when they're provided.
        /// If the type represents a generic type definition, it will not include those.
        /// </summary>
        [JsonPropertyName("genericArguments")]
        public List<string> GenericArguments { get; set; }

        /// <summary>
        /// List of generic parameters and their constraints for generic types.
        /// </summary>
        [JsonPropertyName("genericParameters")]
        public List<GenericParameter> GenericParameters { get; set; }
    }
}
