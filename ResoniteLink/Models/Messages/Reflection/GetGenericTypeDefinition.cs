using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Requests the generic type definition for a particular generic instance type.
    /// E.g. if you have generic type such as MyComponent<int>, this will respond with
    /// a type definition that is the generic definition MyComponent<T>
    /// </summary>
    public class GetGenericTypeDefinition : Message
    {
        /// <summary>
        /// The type of the generic instance for which the generic definition is requested
        /// </summary>
        [JsonPropertyName("genericInstanceType")]
        public string GenericInstanceType { get; set; }
    }
}
