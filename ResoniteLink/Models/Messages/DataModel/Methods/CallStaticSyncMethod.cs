using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class CallStaticSyncMethod : CallSyncMethodBase
    {
        /// <summary>
        /// Datatype on which to call this static method
        /// </summary>
        [JsonPropertyName("targetType")]
        public string TargetType { get; set; }
    }
}
