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
        /// Structured type definition of this component. This is particularly important for generic components, as it will
        /// contain information about the generic parameters and constraints. These generic parameters need to be substituted
        /// for the desired type in the members.
        /// </summary>
        [JsonPropertyName("type")]
        public TypeDefinition Type { get; set; }

        /// <summary>
        /// Indicates of the base type of this component is also a component and should have its bindings generated.
        /// When false, the base type should not be treated like a component anymore.
        /// </summary>
        [JsonPropertyName("baseTypeIsComponent")]
        public bool BaseTypeIsComponent { get; set; }

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
