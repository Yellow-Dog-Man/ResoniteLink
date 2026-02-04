using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class FieldDefinition : MemberDefinition
    {
        /// <summary>
        /// The datatype of the value this field holds. This will typically be primitive types like float, int, bool, float3 and so on
        /// However it can also be a generic parameter for generic container types
        /// </summary>
        [JsonPropertyName("valueType")]
        public TypeReference ValueType { get; set; }
    }
}
