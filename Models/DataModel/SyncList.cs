using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class SyncList : Member
    {
        [JsonPropertyName("elements")]
        public List<Member> Elements { get; set; }
    }

    [JsonDerivedType(typeof(SyncList), "list")]
    public partial class Member { }
}
