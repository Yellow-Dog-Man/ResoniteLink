using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Import a texture asset from a file on the local file system. Note that this must be a file
    /// format supported by Resonite, otherwise this will fail. 
    /// If you are unsure if the file format is supported, send raw texture data instead.
    /// </summary>
    public class ImportTexture2DFile : Message
    {
        /// <summary>
        /// Path of the texture file to import
        /// </summary>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; }
    }
}
