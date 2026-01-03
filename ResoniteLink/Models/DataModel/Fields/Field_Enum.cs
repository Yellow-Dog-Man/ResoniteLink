using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class Field_Enum : Field
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("enumType")]
        public string EnumType { get; set; }
    }

    [JsonDerivedType(typeof(Field_Enum), "enum")]
    public partial class Member { }
}
