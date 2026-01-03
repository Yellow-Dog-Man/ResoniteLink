using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class RemoveComponent : Message
    {
        /// <summary>
        /// The ID of the component that's being removed
        /// </summary>
        [JsonPropertyName("componentId")]
        public string ComponentID { get; set; }
    }
}
