using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Imports texture from raw 8-bit color data. Resonite will take care of encoding the data into a file format.
    /// </summary>
    public class ImportTexture2DRawData : ImportTexture2DRawDataBase<color32>
    {
        /// <summary>
        /// Color profile of the texture color data
        /// </summary>
        [JsonPropertyName("colorProfile")]
        public string ColorProfile { get; set; }
    }
}
