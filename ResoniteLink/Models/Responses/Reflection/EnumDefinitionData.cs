using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class EnumDefinitionData : Response
    {
        /// <summary>
        /// The definition of the enum type
        /// </summary>
        [JsonPropertyName("definition")]
        public EnumDefinition Definition { get; set; }
    }
}
