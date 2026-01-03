using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ComponentData : Response
    {
        /// <summary>
        /// The requested component data
        /// </summary>
        [JsonPropertyName("data")]
        public Component Data { get; set; }
    }
}
