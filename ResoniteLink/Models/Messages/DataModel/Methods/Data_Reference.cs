using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class Data_Reference
    {
        [JsonPropertyName("targetID")]
        public string TargetID { get; set; }
    }

    [JsonDerivedType(typeof(Data_Reference), "reference")]
    public partial class Data { }
}
