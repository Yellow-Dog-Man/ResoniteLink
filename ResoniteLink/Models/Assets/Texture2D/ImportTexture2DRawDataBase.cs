using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract class ImportTexture2DRawDataBase<C> : BinaryPayloadMessage
        where C : unmanaged
    {
        /// <summary>
        /// Width of the texture
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        /// Height of the texture
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        /// Access the raw data of the texture as span. You MUST specify Width & Height properties first.
        /// </summary>
        /// <returns>2D Span allowing access to individual pixels of the texture data</returns>
        /// <exception cref="ArgumentOutOfRangeException">When width or height are invalid or not set</exception>
        /// <exception cref="InvalidOperationException">When width and height change after already accessing data.</exception>
        public unsafe Span2D<C> AccessRawData()
        {
            if (Width <= 0)
                throw new ArgumentOutOfRangeException(nameof(Width));

            if (Height <= 0)
                throw new ArgumentOutOfRangeException(nameof(Height));

            var elements = Width * Height;
            var bytes = sizeof(C) * elements;

            if (RawBinaryPayload == null)
                RawBinaryPayload = new byte[bytes];
            else if (RawBinaryPayload.Length != bytes)
                throw new InvalidOperationException("Width or Height was changed after data was already accessed. This is not supported");

            return new Span2D<C>(Width, Height, MemoryMarshal.Cast<byte, C>(RawBinaryPayload.AsSpan()));
        }
    }
}
