using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract class WorkerDefinition
    {
        /// <summary>
        /// Structured type definition of this component. This is particularly important for generic components, as it will
        /// contain information about the generic parameters and constraints. These generic parameters need to be substituted
        /// for the desired type in the members.
        /// </summary>
        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }

        /// <summary>
        /// List of all members and their definitions that this container has.
        /// </summary>
        [JsonPropertyName("members")]
        public Dictionary<string, MemberDefinition> Members { get; set; }
    }
}
