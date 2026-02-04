using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class GetSyncObjectDefinition : Message
    {
        /// <summary>
        /// The type of the sync object we're fetching definition for.
        /// This MUST be generic type definition for generic sync objects.
        /// </summary>
        [JsonPropertyName("syncObjectType")]
        public string SyncObjectType { get; set; }

        /// <summary>
        /// Flattening sync object definition will include all base class members in the definition as well.
        /// When false, only members declared on the specific type will be returned - you will need to fetch
        /// the base types to construct the whole sync object.
        /// </summary>
        [JsonPropertyName("flattened")]
        public bool Flattened { get; set; }
    }
}
