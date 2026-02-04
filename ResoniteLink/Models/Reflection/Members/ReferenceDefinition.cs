using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ReferenceDefinition : MemberDefinition
    {
        /// <summary>
        /// Datatype of the target of this reference. This is a full type reference, so it can contain other generic arguments/parameters.
        /// </summary>
        [JsonPropertyName("targetType")]
        public TypeReference TargetType { get; set; }
    }
}
