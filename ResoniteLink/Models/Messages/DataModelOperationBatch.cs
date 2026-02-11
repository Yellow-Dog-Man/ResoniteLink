using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Batch of individual data model operation messages. All of these are guaranteed to be processed in sequence
    /// without any engine updates in between. This can prevent the engine updates or user actions affecting the updated objects.
    /// IMPORTANT!!! You can only include messages that derive from DataModelOperation base class.
    /// Other message types cannot be batched, as they are not processed in sync with the data model.
    /// </summary>
    public class DataModelOperationBatch : Message
    {
        // TODO! Is there a good way to enforce the DataModelOperation inheritance with the type system here without making this too weird?
        // We can't just specify it, because then we'd have to duplicate the JsonDerivedType for all messages.
        // We could wrap it with methods and other things, but that is also a bit odd maybe?

        /// <summary>
        /// List of data model operation messages that will be processed in sequence.
        /// IMPORTANT: These must derive from the DataModelOperation base class.
        /// </summary>
        [JsonPropertyName("operations")]
        public List<Message> Operations { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (Operations == null)
                return;

            if (!Operations.All(m => m is DataModelOperation))
                throw new Exception($"Data Model Operation Batch can only include Data Model Operation messages");
        }
    }
}
