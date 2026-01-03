using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class Slot : Worker
    {
        public const string ROOT_SLOT_ID = "Root";

        #region FIELDS

        [JsonPropertyName("parent")]
        public Reference Parent { get; set; }

        [JsonPropertyName("position")]
        public Field_float3 Position { get; set; }

        [JsonPropertyName("rotation")]
        public Field_floatQ Rotation { get; set; }

        [JsonPropertyName("scale")]
        public Field_float3 Scale { get; set; }


        [JsonPropertyName("isActive")]
        public Field_bool IsActive { get; set; }

        [JsonPropertyName("isPersistent")]
        public Field_bool IsPersistent { get; set; }

        [JsonPropertyName("name")]
        public Field_string Name { get; set; }

        [JsonPropertyName("tag")]
        public Field_string Tag { get; set; }

        [JsonPropertyName("orderOffset")]
        public Field_long OrderOffset { get; set; }

        #endregion

        /// <summary>
        /// All the components that belong to this slot.
        /// </summary>
        [JsonPropertyName("components")]
        public List<Component> Components { get; set; }

        /// <summary>
        /// All the children that this slot has
        /// </summary>
        [JsonPropertyName("children")]
        public List<Slot> Children { get; set; }
    }
}
