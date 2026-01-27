using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ListDefinition : MemberDefinition
    {
        /// <summary>
        /// Definition of the elements in this list. Lists contain other members as their elements, all of the same type.
        /// </summary>
        [JsonPropertyName("elementDefinition")]
        public MemberDefinition ElementDefinition { get; set; }
    }
}
