using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class CallSyncMethodBase : DataModelOperation
    {
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
