using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract class AddUpdateSlotData : Message
    {
        /// <summary>
        /// Data of the slot to set/update.
        /// When updating Slot, the ID must be specified.
        /// Any fields that are null will be left as is.
        /// </summary>
        [JsonPropertyName("data")]
        public Slot Data { get; set; }
    }

    public class AddSlot : AddUpdateSlotData
    {

    }

    public class UpdateSlot : AddUpdateSlotData
    {

    }
}
