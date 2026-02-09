using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class Field_Type : Field
    {
        /// <summary>
        /// A data model compatible type encoded into a string.
        /// This uses similar format to how types are specified in C#, but specific to FrooxEngine.
        /// The encoding is same as you get from the reflection API as well.
        /// When setting values, you must take care to use only supported data model types.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonIgnore]
        public override object BoxedValue { get => Type; set => Type = value as string; }

        [JsonIgnore]
        public override Type ValueType => typeof(Type);
    }

    [JsonDerivedType(typeof(Field_Type), "type")]
    public partial class Member { }
}
