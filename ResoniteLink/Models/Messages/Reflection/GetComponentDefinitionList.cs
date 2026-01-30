using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class GetComponentTypeList : Message
    {
        public const string ALL_COMPONENTS = "*";

        /// <summary>
        /// The path in the category list that will be returned.
        /// Null or empty string will return the root categories.
        /// Providing "*" as argument will list ALL components available. Use with caution as this will return a lot of data.
        /// Use forward / slashes for separating categories
        /// </summary>
        [JsonPropertyName("categoryPath")]
        public string CategoryPath { get; set; }
    }
}
