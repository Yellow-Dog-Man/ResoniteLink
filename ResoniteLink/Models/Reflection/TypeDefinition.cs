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
        /// </summary>
        [JsonPropertyName("baseType")]
        public TypeReference BaseType { get; set; }

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
        /// Abstract types cannot ever be instantiated - they are just used as base types for other types
        /// </summary>
        [JsonPropertyName("isAbstract")]
        public bool IsAbstract { get; set; }

        /// <summary>
        /// Indicates if this type is an interface
        /// </summary>
        [JsonPropertyName("isInterface")]
        public bool IsInterface { get; set; }

        /// <summary>
        /// Indicates if this is a generic type. Generic types have generic parameters, which allow to substitute different
        /// types within the type.
        /// </summary>
        [JsonPropertyName("isGenericType")]
        public bool IsGenericType { get; set; }

        /// <summary>
        /// Indicates if this is a definition of a generic type - it represents the "core" type without any generic arguments.
        /// </summary>
        [JsonPropertyName("isGenericTypeDefinition")]
        public bool IsGenericTypeDefinition { get; set; }

        /// <summary>
        /// Number of direct generic parameters on this type. This matters primarily for nested types, where the generic parameters/arguments
        /// can be spread throughout the base classes.
        /// </summary>
        [JsonPropertyName("directGenericParameterCount")]
        public int DirectGenericParameterCount { get; set; }

        /// <summary>
        /// Indicates if this datatype is an engine primitive - one that can be used as value in fields
        /// </summary>
        [JsonPropertyName("isEnginePrimitive")]
        public bool IsEnginePrimitive { get; set; }

        /// <summary>
        /// Indicates if this represents a value type (e.g. struct)
        /// </summary>
        [JsonPropertyName("isValueType")]
        public bool IsValueType { get; set; }

        /// <summary>
        /// Indicates if this datatype is an enum. You can request details about the enum, including its values separately.
        /// </summary>
        [JsonPropertyName("isEnum")]
        public bool IsEnum { get; set; }

        /// <summary>
        /// Indicates if this type represents a component
        /// </summary>
        [JsonPropertyName("isComponent")]
        public bool IsComponent { get; set; }

        /// <summary>
        /// Indicates if this type represents a sync object
        /// </summary>
        [JsonPropertyName("isSyncObject")]
        public bool IsSyncObject { get; set; }

        /// <summary>
        /// Indicates if this type represents a world element - a type that can be referenced by the data model
        /// </summary>
        [JsonPropertyName("isWorldElement")]
        public bool IsWorldElement { get; set; }

        /// <summary>
        /// Indicates if this type is nested within another type definition
        /// </summary>
        [JsonPropertyName("isNested")]
        public bool IsNested { get; set; }

        /// <summary>
        /// When the type is nested, this contains the name of the type that is declaring this particular type.
        /// </summary>
        [JsonPropertyName("declaringType")]
        public string DeclaringType { get; set; }

        /// <summary>
        /// For generic types, this lists all the generic arguments for this type when they're provided.
        /// If the type represents a generic type definition, it will not include those.
        /// This is only populated when the type is a generic type and is NOT a generic type definition.
        /// </summary>
        [JsonPropertyName("genericArguments")]
        public List<TypeReference> GenericArguments { get; set; }

        /// <summary>
        /// List of generic parameters and their constraints for generic types.
        /// This is only populated for generic types.
        /// </summary>
        [JsonPropertyName("genericParameters")]
        public List<GenericParameter> GenericParameters { get; set; }

        /// <summary>
        /// List of interfaces that this type implements. This includes interfaces only specified on the type itself.
        /// You will need to check the base type for inherited interfaces.
        /// </summary>
        [JsonPropertyName("interfaces")]
        public List<TypeReference> Interfaces { get; set; }
    }
}
