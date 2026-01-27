using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class GetEnumDefinition : Message
    {
        /// <summary>
        /// The datatype of the enum that definition is requested for
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
