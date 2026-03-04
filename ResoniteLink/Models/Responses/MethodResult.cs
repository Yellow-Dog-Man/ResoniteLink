using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class MethodResult : Response
    {
        /// <summary>
        /// Data that the method has returned after being called (if any)
        /// </summary>
        [JsonPropertyName("result")]
        public Data Result { get; set; }
    }
}
