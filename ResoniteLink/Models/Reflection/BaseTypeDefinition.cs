using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class BaseTypeDefinition
    {
        /// <summary>
        /// The definition of the base type. If the type is a generic one, it will be generic type definition,
        /// with the generic parameters provided separately.
        /// </summary>
        [JsonPropertyName("definition")]
        public TypeDefinition Definition { get; set; }

        /// <summary>
        /// If the base type is a generic type, this will contain the parameters used specifically by the type.
        /// </summary>
        [JsonPropertyName("genericParameters")]
        public List<TypeDefinition> GenericParameters { get; set; }
    }
}
