using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ComponentList : Response
    {
        /// <summary>
        /// List of components in requested category
        /// </summary>
        [JsonPropertyName("components")]
        public List<ComponentDefinition> Components { get; set; }

        /// <summary>
        /// List of subcategories in the requested category
        /// </summary>
        [JsonPropertyName("subcategories")]
        public List<string> SubCategories { get; set; }
    }
}
