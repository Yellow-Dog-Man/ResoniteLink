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
        /// </summary>
        [JsonPropertyName("valueType")]
        public string ValueType { get; set; }
    }
}
