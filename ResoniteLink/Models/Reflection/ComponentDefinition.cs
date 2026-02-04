using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Definition of a component type
    /// </summary>
    public class ComponentDefinition : WorkerDefinition
    {
        /// <summary>
        /// Indicates of the base type of this component is also a component and should have its bindings generated.
        /// When false, the base type should not be treated like a component anymore.
        /// </summary>
        [JsonPropertyName("baseTypeIsComponent")]
        public bool BaseTypeIsComponent { get; set; }

        /// <summary>
        /// The path where this component is categorized to
        /// </summary>
        [JsonPropertyName("categoryPath")]
        public string CategoryPath { get; set; }
    }
}
