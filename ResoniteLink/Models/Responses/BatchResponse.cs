using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class BatchResponse : Response
    {
        /// <summary>
        /// List of individual responses for a MessageBatch
        /// </summary>
        [JsonPropertyName("responses")]
        public List<Response> Responses { get; set; }
    }
}
