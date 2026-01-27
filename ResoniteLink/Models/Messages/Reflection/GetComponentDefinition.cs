using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class GetComponentDefinition : Message
    {
        /// <summary>
        /// The type of the component we're fetching definition for.
        /// This MUST be generic type definition for generic components.
        /// </summary>
        [JsonPropertyName("componentType")]
        public string ComponentType { get; set; }

        /// <summary>
        /// Flattening component definition will include all base class members in the definition as well.
        /// When false, only members declared on the specific type will be returned - you will need to fetch
        /// the base types to construct the whole component.
        /// </summary>
        [JsonPropertyName("flattened")]
        public bool Flattened { get; set; }
    }
}
