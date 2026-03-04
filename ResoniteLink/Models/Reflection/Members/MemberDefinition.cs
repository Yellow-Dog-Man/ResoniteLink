using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    [JsonDerivedType(typeof(FieldDefinition), "field")]
    [JsonDerivedType(typeof(ReferenceDefinition), "reference")]
    [JsonDerivedType(typeof(ListDefinition), "list")]
    [JsonDerivedType(typeof(ArrayDefinition), "array")]
    [JsonDerivedType(typeof(DictionaryDefinition), "dictionary")]
    [JsonDerivedType(typeof(SyncObjectMemberDefinition), "syncObject")]
    [JsonDerivedType(typeof(EmptyMemberDefinition), "empty")]
    [JsonDerivedType(typeof(SyncPlaybackDefinition), "playback")]
    [JsonDerivedType(typeof(SyncMethodDefinition), "method")]
    public abstract class MemberDefinition
    {
        /// <summary>
        /// The full type of the member itself.
        /// It's recommended to use the subtypes as they provide more structured information for various subtypes when possible.
        /// The full type is however useful when matching up members for references.
        /// </summary>
        [JsonPropertyName("type")]
        public TypeReference Type { get; set; }
    }
}
