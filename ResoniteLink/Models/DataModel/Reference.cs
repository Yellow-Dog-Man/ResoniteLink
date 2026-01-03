using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class Reference : Member
    {
        /// <summary>
        /// The ID of the target that this reference should be set to.
        /// It's important to note that the target needs to be a valid type - it's up to the
        /// caller to ensure that target of correct type is being referenced.
        /// Set to Null to set the reference to null.
        /// </summary>
        [JsonPropertyName("targetId")]
        public string TargetID { get; set; }
    }

    [JsonDerivedType(typeof(Reference), "reference")]
    public partial class Member { }
}
