using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class CallSyncMethod : CallSyncMethodBase
    {
        /// <summary>
        /// ID of the object to call this method on
        /// </summary>
        [JsonPropertyName("targetID")]
        public string TargetID { get; set; }
    }
}
