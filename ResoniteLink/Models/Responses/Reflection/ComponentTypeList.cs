using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class ComponentTypeList : Response
    {
        /// <summary>
        /// List of component types in the requested category
        /// </summary>
        [JsonPropertyName("componentTypes")]
        public List<string> ComponentTypes { get; set; }

        /// <summary>
        /// List of subcategories in the requested category
        /// </summary>
        [JsonPropertyName("subcategories")]
        public List<string> SubCategories { get; set; }
    }
}
