using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ResoniteLink
{
    /// <summary>
    /// Represents messages that operate on the data model - either fetching information or modifying it.
    /// These messages are synchronized with the update loop of the world as soon as possible before being applied.
    /// </summary>
    public abstract class DataModelOperation : Message
    {

    }
}
