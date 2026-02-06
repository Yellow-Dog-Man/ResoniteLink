using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class NewEntityId : Response
    {
        /// <summary>
        /// ID of the newly created entity. This can be useful if you're letting Resonite allocate the ID
        /// You can then use this to fetch the created entity back.
        /// </summary>
        [JsonPropertyName("entityId")]
        public string EntityId { get; set; }
    }
}
