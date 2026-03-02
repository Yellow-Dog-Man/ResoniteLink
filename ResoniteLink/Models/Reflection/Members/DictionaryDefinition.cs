using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class DictionaryDefinition : MemberDefinition
    {
        /// <summary>
        /// Datatype of the key used for the dictionary. This is typically only engine primitives. Most commonly string.
        /// </summary>
        [JsonPropertyName("keyType")]
        public TypeReference KeyType { get; set; }

        /// <summary>
        /// Definition of the elements in this dictionary. Dictionaries contain other members as their elements, all of the same type.
        /// </summary>
        [JsonPropertyName("elementDefinition")]
        public MemberDefinition ElementDefinition { get; set; }
    }
}
