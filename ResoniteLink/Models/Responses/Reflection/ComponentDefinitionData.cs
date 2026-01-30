using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ComponentDefinitionData : Response
    {
        /// <summary>
        /// Definition of the component that was requested
        /// </summary>
        [JsonPropertyName("definition")]
        public ComponentDefinition Definition { get; set; }
    }
}
