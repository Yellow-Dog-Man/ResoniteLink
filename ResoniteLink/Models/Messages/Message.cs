using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Base class for any messages/commands sent to Resonite
    /// </summary>
    [JsonDerivedType(typeof(GetSlot), "getSlot")]
    [JsonDerivedType(typeof(AddSlot), "addSlot")]
    [JsonDerivedType(typeof(UpdateSlot), "updateSlot")]
    [JsonDerivedType(typeof(RemoveSlot), "removeSlot")]

    [JsonDerivedType(typeof(GetComponent), "getComponent")]
    [JsonDerivedType(typeof(AddComponent), "addComponent")]
    [JsonDerivedType(typeof(UpdateComponent), "updateComponent")]
    [JsonDerivedType(typeof(RemoveComponent), "removeComponent")]

    [JsonDerivedType(typeof(ImportTexture2DFile), "importTexture2DFile")]
    [JsonDerivedType(typeof(ImportTexture2DRawData), "importTexture2DRawData")]
    [JsonDerivedType(typeof(ImportTexture2DRawDataHDR), "importTexture2DRawDataHDR")]

    [JsonDerivedType(typeof(ImportMeshJSON), "importMeshJSON")]
    [JsonDerivedType(typeof(ImportMeshRawData), "importMeshRawData")]
    public abstract class Message
    {
        /// <summary>
        /// Unique ID of this message. This can be used to match the response.
        /// </summary>
        [JsonPropertyName("messageId")]
        public string MessageID { get; set; }
    }
}
