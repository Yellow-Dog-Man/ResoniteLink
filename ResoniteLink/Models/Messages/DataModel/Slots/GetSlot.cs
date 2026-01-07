using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class GetSlot : Message
    {
        /// <summary>
        /// Unique ID of the slot we're requesting data for.
        /// Special case: "Root" will fetch the root slot of the world.
        /// </summary>
        [JsonPropertyName("slotId")]
        public string SlotID { get; set; }

        /// <summary>
        /// How deep to fetch the hierarchy.
        /// Value of 0 will fetch only the requested slot fully.
        /// Value of 1 will fully fetch the immediate children.
        /// Value of -1 will fetch everything fully.
        /// Any immediate children of slots beyond this depth will be fetched as references only.
        /// </summary>
        [JsonPropertyName("depth")]
        public int Depth { get; set; }

        /// <summary>
        /// Indicates if components should be fetched fully with all their data or only as references.
        /// Set to False if you plan on fetching the individual component data later.
        /// </summary>
        [JsonPropertyName("includeComponentData")]
        public bool IncludeComponentData { get; set; }
    }
}
