using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class GetTypeDefinition : Message
    {
        /// <summary>
        /// Full name of the type that we are requesting definition for
        /// This can be both a generic type definition or a specific constructed generic type.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
