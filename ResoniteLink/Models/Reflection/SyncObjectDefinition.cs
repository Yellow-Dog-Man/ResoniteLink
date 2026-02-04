using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Definition of a sync object type
    /// </summary>
    public class SyncObjectDefinition : WorkerDefinition
    {
        /// <summary>
        /// Indicates of the base type of this sync object is also a sync object and should have its bindings generated.
        /// When false, the base type should not be treated like a sync object anymore.
        /// </summary>
        [JsonPropertyName("baseTypeIsSyncObject")]
        public bool BaseTypeIsSyncObject { get; set; }
    }
}
