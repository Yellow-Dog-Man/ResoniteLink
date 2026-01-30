using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class TypeDefinitionData : Response
    {
        /// <summary>
        /// The structured definition of the type
        /// </summary>
        [JsonPropertyName("definition")]
        public TypeDefinition Definition { get; set; }
    }
}
