using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Response from Resonite to a message that has been sent. This can simply indicate success/failure
    /// or contain response data when requested.
    /// </summary>
    [JsonDerivedType(typeof(Response), "response")]
    [JsonDerivedType(typeof(NewEntityId), "newEntityId")]
    [JsonDerivedType(typeof(SlotData), "slotData")]
    [JsonDerivedType(typeof(ComponentData), "componentData")]
    [JsonDerivedType(typeof(AssetData), "assetData")]
    [JsonDerivedType(typeof(SessionData), "sessionData")]

    [JsonDerivedType(typeof(TypeDefinitionData), "typeDefinitionData")]
    [JsonDerivedType(typeof(EnumDefinitionData), "enumDefinitionData")]
    [JsonDerivedType(typeof(ComponentDefinitionData), "componentDefinitionData")]
    [JsonDerivedType(typeof(SyncObjectDefinitionData), "syncObjectDefinitionData")]
    [JsonDerivedType(typeof(ComponentTypeList), "componentTypeList")]
    public class Response
    {
        /// <summary>
        /// Unique ID of the message that this response is to.
        /// </summary>
        [JsonPropertyName("sourceMessageId")]
        public string SourceMessageID { get; set; }

        /// <summary>
        /// Indicates if the request succeeded or failed. When false, check the error.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Contains details on why the request failed.
        /// </summary>
        [JsonPropertyName("errorInfo")]
        public string ErrorInfo { get; set; }
    }
}
