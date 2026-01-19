using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Definition of a component type
    /// </summary>
    public class ComponentDefinition
    {
        /// <summary>
        /// The type of the container itself.
        /// </summary>
        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }

        /// <summary>
        /// List of all members and their definitions that this container has.
        /// </summary>
        [JsonPropertyName("members")]
        public Dictionary<string, MemberDefinition> Members { get; set; }

        /// <summary>
        /// The path where this component is categorized to
        /// </summary>
        [JsonPropertyName("categoryPath")]
        public string CategoryPath { get; set; }
    }
}
