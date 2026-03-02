using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public abstract class SyncDictionary : Member
    {
        [JsonIgnore]
        public abstract Type KeyType { get; }
    }

    public abstract class SyncDictionary<TKey> : SyncDictionary
    {
        [JsonIgnore]
        public override Type KeyType => typeof(TKey);

        [JsonPropertyName("elements")]
        public Dictionary<TKey, Member> Elements { get; set; }
    }

    public class SyncDictionary_Enum : SyncDictionary<string>
    {
        [JsonPropertyName("enumType")]
        public string EnumType { get; set; }
    }

    [JsonDerivedType(typeof(SyncDictionary_Enum), "dictionary<enum>")]
    public partial class Member { }
}
