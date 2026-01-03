using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Request for full data of a particular component
    /// </summary>
    public class GetComponent : Message
    {
        /// <summary>
        /// The ID of the component that's being fetched
        /// </summary>
        [JsonPropertyName("componentId")]
        public string ComponentID { get; set; }
    }
}
