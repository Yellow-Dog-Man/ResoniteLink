namespace ResoniteLink
{
    /// <summary>
    /// Commands supported by ResoniteLink
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// `addChild` - Adds a child slot to the current slot
        /// </summary>
        AddChild,
        /// <summary>
        /// `addComponent` - Adds a component to the current slot
        /// </summary>
        AddComponent,
        /// <summary>
        /// `clearComponent` - Clears the current component
        /// </summary>
        ClearComponent,
        /// <summary>
        /// `componentState` - Gets the state of the current component
        /// </summary>
        ComponentState,
        /// <summary>
        /// `echo` - Echoes a message
        /// </summary>
        Echo,
        /// <summary>
        /// `exit` - Disconnects and exits the REPL
        /// </summary>
        Exit,
        /// <summary>
        /// `importTexture` - Imports a texture to the current slot
        /// </summary>
        ImportTexture,
        /// <summary>
        /// `listChildren` - Lists child slots of the current slot
        /// </summary>
        ListChildren,
        /// <summary>
        /// `listComponents` - Lists components of the current slot
        /// </summary>
        ListComponents,
        /// <summary>
        /// `removeComponent` - Removes the current component
        /// </summary>
        RemoveComponent,
        /// <summary>
        /// `removeSlot` - Removes the current slot
        /// </summary>
        RemoveSlot,
        /// <summary>
        /// `selectChild` - Selects a child slot by index
        /// </summary>
        SelectChild,
        /// <summary>
        /// `selectComponent` - Selects a component by index
        /// </summary>
        SelectComponent,
        /// <summary>
        /// `selectParent` - Selects the parent slot of the current slot
        /// </summary>
        SelectParent,
        /// <summary>
        /// `setMember` - Sets a member of the current component
        /// </summary>
        SetMember,
        /// <summary>
        /// Unknown command, used to indicate unrecognized input
        /// </summary>
        Unknown,
    }
}