using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Represents a response to a batch of messages, e.g DataModelOperationBatch, containing individual responses to each.
    /// IMPORTANT: Note that Success on this message itself only indicates that the batch itself was processed successfully,
    /// but not necessarily that each individual message has succeeded. You need to check each individual response for this.
    /// </summary>
    public class BatchResponse : Response
    {
        /// <summary>
        /// List of individual responses for a MessageBatch
        /// </summary>
        [JsonPropertyName("responses")]
        public List<Response> Responses { get; set; }
    }
}
