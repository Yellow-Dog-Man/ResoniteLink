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
        /// Full typename of the component. This is the same type that can be used to instantiate the component.
        /// Be careful with generic types - those require specifying generic arguments as well when instantiating.
        /// You can resolve this type to get TypeDefinition which contains structured information that will let you
        /// construct the full typename.
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
    }
}
