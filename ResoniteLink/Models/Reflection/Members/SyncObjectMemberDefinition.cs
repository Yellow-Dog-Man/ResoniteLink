using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// SyncObjects are entities that are members of a container (like a component), which contain their own members.
    /// This indicates that a sync member is embedded as a member
    /// </summary>
    public class SyncObjectMemberDefinition : MemberDefinition
    {
        /// <summary>
        /// Datatype of the sync object
        /// </summary>
        [JsonPropertyName("type")]
        public TypeReference Type { get; set; }
    }
}
