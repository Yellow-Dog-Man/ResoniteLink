using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ResoniteLinkSession
    {
        /// <summary>
        /// Name of the session
        /// </summary>
        [JsonPropertyName("sessionName")]
        public string SessionName { get; set; }

        /// <summary>
        /// Unique ID of the session. This never changes over the course of the session, while SessionName can.
        /// </summary>
        [JsonPropertyName("sessionID")]
        public string SessionId { get; set; }

        /// <summary>
        /// Port on which ResoniteLink is hosted on
        /// </summary>
        [JsonPropertyName("linkPort")]
        public int LinkPort { get; set; }

        /// <summary>
        /// IP endpoint in which ResoniteLink is hosted. Typically this will be localhost
        /// </summary>
        [JsonIgnore]
        public IPEndPoint LinkEndPoint { get; set; }

        /// <summary>
        /// Timestamp of the last update of this session
        /// </summary>
        [JsonIgnore]
        public DateTime LastUpdateTimestamp { get; set; }
    }
}
