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

        [JsonIgnore]
        public override object BoxedValue { get => Value; set => Value = value as string; }

        // We don't actually have the enum type, so we just give general enum type
        [JsonIgnore]
        public override Type ValueType => typeof(Enum);
    }

    public class Field_Nullable_Enum : Field
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("enumType")]
        public string EnumType { get; set; }

        [JsonIgnore]
        public override object BoxedValue { get => Value; set => Value = value as string; }

        // We don't actually have the enum type, so we just give general enum type
        [JsonIgnore]
        public override Type ValueType => typeof(Enum);
    }

    [JsonDerivedType(typeof(Field_Enum), "enum")]
    [JsonDerivedType(typeof(Field_Nullable_Enum), "enum?")]
    public partial class Member { }
}
