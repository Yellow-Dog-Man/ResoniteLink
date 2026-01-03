using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract class Worker
    {
        /// <summary>
        /// Unique ID of this worker. This can be used to reference it from other places.
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; }

        /// <summary>
        /// When true, this instance doesn't contain full data, but only serves as a reference
        /// to this worker existing.
        /// </summary>
        [JsonPropertyName("isReferenceOnly")]
        public bool IsReferenceOnly { get; set; }
    }
}
