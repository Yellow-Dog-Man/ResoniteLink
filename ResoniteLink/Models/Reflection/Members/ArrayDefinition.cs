using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ArrayDefinition : MemberDefinition
    {
        /// <summary>
        /// The datatype of each element in the array that it holds. Typically primitives like float, int bool, float3 and so on
        /// However it can also be a generic parameter for generic container types
        /// </summary>
        [JsonPropertyName("valueType")]
        public TypeReference ValueType { get; set; }
    }
}
