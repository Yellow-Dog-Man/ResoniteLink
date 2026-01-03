using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class RemoveSlot : Message
    {
        /// <summary>
        /// The ID of the slot to remove.
        /// </summary>
        [JsonPropertyName("slotId")]
        public string SlotID { get; set; }
    }
}
