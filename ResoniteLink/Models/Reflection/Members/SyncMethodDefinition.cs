using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    public class SyncMethodDefinition : MemberDefinition
    {
        /// <summary>
        /// Definition of parameters of this method. If empty, the method has none.
        /// </summary>
        [JsonPropertyName("parameters")]
        public Dictionary<string, TypeReference> Parameters { get; set; } = new Dictionary<string, TypeReference>();

        /// <summary>
        /// Datatype that this method returns after called.
        /// Note: Since Resonite link is asynchronous API, both sync & async methods will look the same and return same type.
        /// E.g. for both "bool MyMethod()" and "Task<bool> Mymethod()" you will get "bool" here.
        /// </summary>
        [JsonPropertyName("returnType")]
        public TypeReference ReturnType { get; set; }

        /// <summary>
        /// Indicates if this method is sync or async.
        /// This doesn't make difference in how you call those methods through ResoniteLink, however it does make a difference
        /// in how they can repspond - sync methods will be called and respond typically as soon as possible and in sequence you called them.
        /// However async methods might take longer time to do their work and you might get response from them out of order.
        /// </summary>
        [JsonPropertyName("isAsync")]
        public bool IsAsync { get; set; }
    }
}
