using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class CallSyncMethod : DataModelOperation
    {
        /// <summary>
        /// ID of the object to call this method on
        /// </summary>
        [JsonPropertyName("targetID")]
        public string TargetID { get; set; }

        /// <summary>
        /// Name of the method to call
        /// </summary>
        [JsonPropertyName("methodName")]
        public string MethodName { get; set; }

        /// <summary>
        /// Arguments to pass to the method
        /// </summary>
        [JsonPropertyName("arguments")]
        public Dictionary<string, Data> Arguments { get; set; } = new Dictionary<string, Data>();
    }
}
