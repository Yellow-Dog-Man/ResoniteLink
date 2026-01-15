using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class SessionData : Response
    {
        /// <summary>
        /// Version number of Resonite
        /// </summary>
        [JsonPropertyName("resoniteVersion")]
        public string ResoniteVersion { get; set; }

        /// <summary>
        /// Version number of ResoniteLink library that Resonite uses
        /// </summary>
        [JsonPropertyName("resoniteLinkVersion")]
        public string ResoniteLinkVersion { get; set; }

        /// <summary>
        /// An ID uniquely identifying this ResoniteLink session for a given Resonite session
        /// The ID is unique for as long as particular session runs on Resonite's end
        /// The ID is NOT guaranteed to be unique for different Resonite worlds with ResoniteLink enabled
        /// The ID is NOT guaranteed to be unique when the Resonite world restarts
        /// You can use this ID to ensure that any ID's you generate do not conflict with any other
        /// ResoniteLink session for a given Resonite world.
        /// </summary>
        [JsonPropertyName("uniqueSessionId")]
        public string UniqueSessionId { get; set; }
    }
}
