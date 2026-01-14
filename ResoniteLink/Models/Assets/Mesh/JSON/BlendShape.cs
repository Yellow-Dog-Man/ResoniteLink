using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class BlendShape
    {
        /// <summary>
        /// Name of the Blendshape
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Frames that compose this blendshape
        /// Blendshapes need at least 1 frame
        /// </summary>
        [JsonPropertyName("frames")]
        public List<BlendshapeFrame> Frames { get; set; }
    }
}
