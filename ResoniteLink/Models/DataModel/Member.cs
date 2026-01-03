using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract partial class Member
    {
        /// <summary>
        /// Unique ID of this member. Can be used for anything needing to reference this member.
        /// </summary>
        [JsonPropertyName("id")]
        public string ID { get; set; }
    }
}
