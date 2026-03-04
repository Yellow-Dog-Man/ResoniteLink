using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract class DataValue : Data
    {
        [JsonIgnore]
        public abstract Type ValueType { get; }

        [JsonIgnore]
        public abstract object BoxedValue { get; set; }
    }
}
