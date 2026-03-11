using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class Field_SphericalHarmonicsL1 : Field
    {
        [JsonPropertyName("value")]
        public SphericalHarmonicsL1 Value { get; set; }

        [JsonIgnore]
        public override object BoxedValue { get => Value; set => Value = (SphericalHarmonicsL1)value; }
        [JsonIgnore]
        public override Type ValueType => typeof(SphericalHarmonicsL1);
    }

    public class Field_SphericalHarmonicsL2 : Field
    {
        [JsonPropertyName("value")]
        public SphericalHarmonicsL2 Value { get; set; }

        [JsonIgnore]
        public override object BoxedValue { get => Value; set => Value = (SphericalHarmonicsL2)value; }
        [JsonIgnore]
        public override Type ValueType => typeof(SphericalHarmonicsL2);
    }

    public class Field_SphericalHarmonicsL3 : Field
    {
        [JsonPropertyName("value")]
        public SphericalHarmonicsL3 Value { get; set; }

        [JsonIgnore]
        public override object BoxedValue { get => Value; set => Value = (SphericalHarmonicsL3)value; }
        [JsonIgnore]
        public override Type ValueType => typeof(SphericalHarmonicsL3);
    }

    public class Field_SphericalHarmonicsL4 : Field
    {
        [JsonPropertyName("value")]
        public SphericalHarmonicsL4 Value { get; set; }

        [JsonIgnore]
        public override object BoxedValue { get => Value; set => Value = (SphericalHarmonicsL4)value; }
        [JsonIgnore]
        public override Type ValueType => typeof(SphericalHarmonicsL4);
    }

    [JsonDerivedType(typeof(Field_SphericalHarmonicsL1), "sh_l1")]
    [JsonDerivedType(typeof(Field_SphericalHarmonicsL2), "sh_l2")]
    [JsonDerivedType(typeof(Field_SphericalHarmonicsL3), "sh_l3")]
    [JsonDerivedType(typeof(Field_SphericalHarmonicsL4), "sh_l4")]
    public partial class Member { }
}
