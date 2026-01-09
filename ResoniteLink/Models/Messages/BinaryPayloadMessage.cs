using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Represents a message with a binary payload. This payload is sent as a separate WebSocket binary
    /// message that follows immediatelly after this one.
    /// </summary>
    public abstract class BinaryPayloadMessage : Message
    {
        [JsonIgnore]
        public byte[] RawBinaryPayload { get; set; }
    }
}
