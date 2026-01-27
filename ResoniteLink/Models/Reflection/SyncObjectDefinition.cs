using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// SyncObjects are entities that are members of a container (like a component), which contain their own members
    /// </summary>
    public class SyncObjectDefinition : MemberDefinition
    {
        /// <summary>
        /// List of all members and their definitions that this container has.
        /// </summary>
        [JsonPropertyName("members")]
        public Dictionary<string, MemberDefinition> Members { get; set; }
    }
}
