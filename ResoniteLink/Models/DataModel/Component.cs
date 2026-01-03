using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class Component : Worker
    {
        /// <summary>
        /// Datatype of the component, specified using Resonite's notation (equivalent to the C# notation)
        /// This does not need to be specified when updating existing component, as the type cannot be
        /// updated over components lifetime.
        /// </summary>
        [JsonPropertyName("componentType")]
        public string ComponentType { get; set; }

        /// <summary>
        /// Members (fields, references, lists...) of this component and their data
        /// </summary>
        [JsonPropertyName("members")]
        public Dictionary<string, Member> Members { get; set; }
    }
}
