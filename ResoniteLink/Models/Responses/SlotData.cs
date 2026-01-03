using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class SlotData : Response
    {
        /// <summary>
        /// Depth of the requested data (this is same as requested, included for reference).
        /// </summary>
        [JsonPropertyName("depth")]
        public int Depth { get; set; }

        /// <summary>
        /// The requested slot data
        /// </summary>
        [JsonPropertyName("data")]
        public Slot Data { get; set; }
    }
}
