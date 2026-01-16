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
        /// The type of the container itself. Includes assembly name, namespace and generic parameters
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

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

        /// <summary>
        /// For generic types, this contains information about their generic arguments.
        /// </summary>
        [JsonPropertyName("genericArguments")]
        public Dictionary<string, GenericArgumentDefinition> GenericArguments { get; set; }
    }
}
