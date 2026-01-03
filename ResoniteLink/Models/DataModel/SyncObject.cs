using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class SyncObject : Member
    {
        /// <summary>
        /// Members (fields, references, lists...) of this sync object and their data
        /// </summary>
        [JsonPropertyName("members")]
        public Dictionary<string, Member> Members { get; set; }
    }

    [JsonDerivedType(typeof(SyncObject), "syncObject")]
    public partial class Member { }
}
