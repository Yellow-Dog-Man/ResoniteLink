using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Import a audio clip asset from a file on the local file system. Note that this must be a file
    /// format supported by Resonite, otherwise this will fail. 
    /// If you are unsure if the file format is supported, send raw audio data instead.
    /// Generally WAV, OGG & FLAC files are supported as audio clips.
    /// </summary>
    public class ImportAudioClipFile : Message
    {
        /// <summary>
        /// Path of the audio clip file to import
        /// </summary>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; }
    }
}
