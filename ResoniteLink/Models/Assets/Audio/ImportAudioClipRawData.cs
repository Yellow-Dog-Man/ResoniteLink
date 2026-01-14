using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ImportAudioClipRawData : BinaryPayloadMessage
    {
        /// <summary>
        /// Number of audio samples
        /// </summary>
        [JsonPropertyName("audioSampleCount")]
        public int AudioSampleCount { get; set; }

        /// <summary>
        /// Sample rate of the audio data
        /// </summary>
        [JsonPropertyName("sampleRate")]
        public int SampleRate { get; set; }

        /// <summary>
        /// Number of audio channels. 1 mono, 2 stereo, 6 is 5.1 surround
        /// It's your responsibility to make sure that Resonite supports given audio channel count
        /// The actual audio sample data is interleaved in the buffer
        /// </summary>
        [JsonPropertyName("channelCount")]
        public int ChannelCount { get; set; }

        /// <summary>
        /// The duration of the audio clip in seconds, computed from the sample count and sample rate.
        /// This is just convenience property, setting it will update AudioSampleCount accordingly.
        /// </summary>
        [JsonIgnore]
        public double Duration
        {
            get => AudioSampleCount / (double)SampleRate;
            set
            {
                if(SampleRate <= 0)
                    throw new InvalidOperationException("You must set SampleRate before setting Duration");

                AudioSampleCount = (int)(value * SampleRate);
            }
        }

        public unsafe Span<float> AccessRawData()
        {
            if (AudioSampleCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(AudioSampleCount));

            if(SampleRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(SampleRate));

            if (ChannelCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(ChannelCount));

            var totalCount = AudioSampleCount * ChannelCount;
            var bytes = sizeof(float) * totalCount;

            if (RawBinaryPayload == null)
                RawBinaryPayload = new byte[bytes];
            else if (RawBinaryPayload.Length != bytes)
                throw new InvalidOperationException("Properties were modified after accessing raw data. This is not supported");

            return MemoryMarshal.Cast<byte, float>(RawBinaryPayload.AsSpan());
        }
    }
}
