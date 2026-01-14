using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class BlendShapeRawData
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
        public List<BlendShapeFrameRawData> Frames { get; set; }

        internal void ComputeBufferOffsets(ImportMeshRawData mesh, ref int offset)
        {
            foreach (var frame in Frames)
                frame.ComputeBufferOffsets(mesh, ref offset);
        }

        internal void AssignBuffer(byte[] buffer)
        {
            foreach (var frame in Frames)
                frame.AssignBuffer(buffer);
        }
    }
}
