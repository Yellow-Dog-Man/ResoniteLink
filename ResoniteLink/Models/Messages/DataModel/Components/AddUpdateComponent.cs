using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract class AddUpdateComponent : Message
    {
        /// <summary>
        /// The state of the component data. Any members that are not included will be left as is.
        /// When updating the component, the ID must be specified!
        /// </summary>
        [JsonPropertyName("data")]
        public Component Data { get; set; }
    }

    public class AddComponent : AddUpdateComponent
    {
        /// <summary>
        /// The ID of the Slot that this component should be added to.
        /// </summary>
        [JsonPropertyName("containerSlotId")]
        public string ContainerSlotId { get; set; }
    }

    public class UpdateComponent : AddUpdateComponent
    {

    }
}
