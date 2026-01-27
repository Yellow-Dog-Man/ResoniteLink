using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ReferenceDefinition : MemberDefinition
    {
        /// <summary>
        /// Datatype of the target of this reference. This is a fully specified type including assembly name and generic parameters.
        /// </summary>
        [JsonPropertyName("targetType")]
        public string TargetType { get; set; }
    }
}
