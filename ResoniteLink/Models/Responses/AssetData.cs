using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class AssetData : Response
    {
        /// <summary>
        /// URL of the imported asset. This can be assigned to static asset providers.
        /// Note: Usually this URL is valid only within the session. It is NOT recommended to persist it
        /// outside of the ResoniteLink session - static asset providers will automatically update the URL
        /// when the world/item is saved.
        /// </summary>
        [JsonPropertyName("assetURL")]
        public Uri AssetURL { get; set; }
    }
}
