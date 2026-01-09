using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ResoniteLink
{
    public class REPL_Controller
    {
        readonly LinkInterface _link;
        readonly ICommandIO _messaging;

        public Slot CurrentSlot { get; private set; }
        public Component CurrentComponent { get; private set; }

        // The prefix prevents multiple REPL sessions from colliding with each other's ID's
        readonly string _prefix;

        int _idPool;

        string AllocateId() => $"REPL_{_prefix}_{_idPool++:X}";

        public REPL_Controller(LinkInterface link, ICommandIO messaging, string prefix = null)
        {
            _link = link;
            _messaging = messaging;

            if (prefix == null)
            {
                // If none is provided, just generate one at random
                // This isn't the most robust, as it can collide, but usually there's only a handful sessions at most
                // so this should work well enough for these purposes
                var r = new Random();
                _prefix = r.Next().ToString("X");
            }
            else
                _prefix = prefix;
        }

        public async Task RunLoop()
        {
            // Make sure we start with the root slot selected
            if (CurrentSlot == null)
                await SelectSlot(Slot.ROOT_SLOT_ID);

            bool keepProcessing;

            do
            {
                await PrintPrompt();

                var command = await CommandParser.ReadCommand();

                keepProcessing = await ProcessCommand(command);
            } while (keepProcessing);
        }

        async Task PrintPrompt()
        {
            var str = new StringBuilder();

            str.Append($"Slot: {CurrentSlot.Name.Value} (ID: {CurrentSlot.ID})");

            if (CurrentComponent != null)
                str.Append($" Component: {CurrentComponent.ComponentType} (ID: {CurrentComponent.ID})");

            await _messaging.PrintPrompt(str.ToString());
        }
        
        async Task<bool> ProcessCommand(Command command)
        {
            var commandType = command.CommandType;
            var arguments = command.Arguments;

            switch(commandType)
            {
                case CommandType.Echo:
                    // Not necessary, but a good for sanity check
                    await _messaging.PrintLine(arguments);
                    break;

                case CommandType.ListChildren:
                    await RefreshCurrent();

                    await _messaging.PrintLine("Children count: " + (CurrentSlot.Children?.Count ?? 0));

                    for(int i = 0; i < (CurrentSlot.Children?.Count ?? 0); i++)
                    {
                        var child = CurrentSlot.Children[i];
                        await _messaging.PrintLine($"\t[{i}] {child.Name.Value} (ID: {child.ID})");
                    }

                    break;

                case CommandType.ListComponents:
                    await RefreshCurrent();

                    await _messaging.PrintLine("Component count: " + (CurrentSlot.Components?.Count ?? 0));

                    for (int i = 0; i < (CurrentSlot.Components?.Count ?? 0); i++)
                    {
                        var component = CurrentSlot.Components[i];
                        await _messaging.PrintLine($"\t[{i}] {component.ComponentType} (ID: {component.ID})");
                    }
                    break;

                case CommandType.SelectChild:
                    if(!int.TryParse(arguments, out var childIndex))
                    {
                        await _messaging.PrintError("Could not parse child index");
                        break;
                    }

                    if(childIndex < 0 || childIndex >= (CurrentSlot.Children?.Count ?? 0))
                    {
                        await _messaging.PrintError("Child Index is out of range");
                        break;
                    }

                    await SelectSlot(CurrentSlot.Children[childIndex].ID);

                    break;

                case CommandType.SelectComponent:
                    if (!int.TryParse(arguments, out var componentIndex))
                    {
                        await _messaging.PrintError("Could not parse component index");
                        break;
                    }

                    if (componentIndex < 0 || componentIndex >= (CurrentSlot.Components?.Count ?? 0))
                    {
                        await _messaging.PrintError("Component Index is out of range");
                        break;
                    }

                    await SelectComponent(CurrentSlot.Components[componentIndex].ID);
                    break;

                case CommandType.ClearComponent:
                    CurrentComponent = null;
                    break;

                case CommandType.ComponentState:
                    await RefreshCurrent();

                    if (CurrentComponent == null)
                    {
                        await _messaging.PrintError("No component is selected");
                        break;
                    }

                    await PrintComponentMembers();
                    break;

                case CommandType.AddComponent:
                    if (string.IsNullOrWhiteSpace(arguments))
                    {
                        await _messaging.PrintError("You must provide type of the component");
                        break;
                    }

                    await RefreshCurrent();

                    var componentId = await AddComponent(arguments);

                    if (componentId != null)
                        await _messaging.PrintLine($"Added! ID: {componentId}");
                    break;

                case CommandType.AddChild:
                    if(string.IsNullOrWhiteSpace(arguments))
                    {
                        await _messaging.PrintError("You must provide a name of the child");
                        break;
                    }

                    // Add the child
                    var childId = await AddChild(arguments.Trim());

                    // Immediatelly select the new child
                    if (childId != null)
                        await _messaging.PrintLine($"Child added. ID: {childId}");
                    break;

                case CommandType.RemoveSlot:
                    await RemoveCurrentSlot();
                    break;

                case CommandType.RemoveComponent:
                    string idToRemove;

                    if(string.IsNullOrWhiteSpace(arguments))
                    {
                        if(CurrentComponent == null)
                        {
                            await _messaging.PrintError("No component is currently selected. Either select component first or provide index of component to remove.");
                            break;
                        }

                        idToRemove = CurrentComponent.ID;
                    }
                    else
                    {
                        if (!int.TryParse(arguments, out componentIndex))
                        {
                            await _messaging.PrintError("Could not parse component index");
                            break;
                        }

                        if (componentIndex < 0 || componentIndex >= (CurrentSlot.Components?.Count ?? 0))
                        {
                            await _messaging.PrintError("Component Index is out of range");
                            break;
                        }

                        idToRemove = CurrentSlot.Components[componentIndex].ID;
                    }

                    await RemoveComponent(idToRemove);
                    break;

                case CommandType.SelectParent:
                    if(CurrentSlot.Parent.TargetID == null)
                    {
                        await _messaging.PrintError("Root is topmost slot, cannot select parent");
                        break;
                    }

                    await SelectSlot(CurrentSlot.Parent.TargetID);

                    break;

                case CommandType.SetMember:
                    if(CurrentComponent == null)
                    {
                        await _messaging.PrintError("No component is selected");
                        break;
                    }

                    CommandParser.SplitCommand(arguments, out var memberName, out var setValue);

                    if(setValue == null)
                    {
                        await _messaging.PrintError("Invalid number of arguments. Usage: set <MemberName> <Value as JSON>");
                        break;
                    }

                    await SetMember(memberName, setValue);
                    break;

                case CommandType.ImportTexture:
                    if(string.IsNullOrWhiteSpace(arguments))
                    {
                        Console.WriteLine("You must provide a path to the file");
                        break;
                    }

                    var path = System.IO.Path.GetFullPath(arguments);

                    if(!System.IO.File.Exists(path))
                    {
                        Console.WriteLine("File does not exist");
                        break;
                    }

                    await ImportTexture(path);
                    break;

                case CommandType.Exit:
                    // We stop processing
                    return false;

                default:
                    // Unknown command
                    await _messaging.PrintError($"Unknown command: '{arguments}'");
                    break;
            }

            return true;
        }

        async Task RefreshCurrent()
        {
            if (CurrentComponent != null)
                await SelectComponent(CurrentComponent.ID);
            else
                await SelectSlot(CurrentSlot.ID);
        }

        async Task SelectSlot(string slotID)
        {
            // Fetch information about the slot we selected
            var result = await _link.GetSlotData(new GetSlot()
            {
                SlotID = slotID,
                Depth = 0,
                IncludeComponentData = false,
            });

            // If we failed (e.g. slot can be deleted in the meanwhile or the ID is wrong), we reset back to root
            if(!result.Success)
            {
                await _messaging.PrintError($"Error! Resetting back to root");
                await SelectSlot(Slot.ROOT_SLOT_ID);
            }

            CurrentSlot = result.Data;
            CurrentComponent = null;
        }

        async Task SelectComponent(string componentId)
        {
            // Fetch information about the slot we selected
            var result = await _link.GetComponentData(new GetComponent()
            {
                ComponentID = componentId,
            });

            // If we failed (e.g. slot can be deleted in the meanwhile or the ID is wrong), we reset back to root
            if (!result.Success)
            {
                await _messaging.PrintError($"Error! Failed to fetch component data: {result.ErrorInfo}");
                return;
            }

            CurrentComponent = result.Data;

            await PrintComponentMembers();
        }

        async Task<string> AddComponent(string type)
        {
            var componentId = AllocateId();

            var result = await _link.AddComponent(new AddComponent()
            {
                ContainerSlotId = CurrentSlot.ID,
                Data = new Component()
                {
                    ComponentType = type.Trim(),
                    ID = componentId
                }
            });

            if (result.Success)
                return componentId;
            else
            {
                await _messaging.PrintError($"Failed to add component: " + result.ErrorInfo);
                return null;
            }
        }

        async Task<string> AddChild(string name)
        {
            // We allocate our own ID, so we can immediatelly select it after without having to fetch it back
            var childId = AllocateId();

            var result = await _link.AddSlot(new AddSlot()
            {
                Data = new Slot()
                {
                    // If this was left out, Resonite will allocate its own ID which we would not know
                    // without fetching it back. But we can force a custom ID to avoid this!
                    ID = childId,

                    // We set the current slot as the parent
                    Parent = new Reference() { TargetID = CurrentSlot.ID },

                    // Set the new name immediatelly
                    Name = new Field_string() { Value = name },
                }
            });

            if (result.Success)
                return childId;
            else
            {
                await _messaging.PrintError($"Failed to add child: " + result.ErrorInfo);
                return null;
            }
        }

        async Task RemoveCurrentSlot()
        {
            var result = await _link.RemoveSlot(new RemoveSlot()
            {
                SlotID = CurrentSlot.ID,
            });

            if(!result.Success)
            {
                await _messaging.PrintError($"Failed to remove slot: {result.ErrorInfo}");
                return;
            }

            // Select the parent
            await SelectSlot(CurrentSlot.Parent.TargetID);
        }

        async Task PrintComponentMembers()
        {
            if (CurrentComponent == null)
                throw new InvalidOperationException("No component is currently selected");

            if(CurrentComponent == null)
            {
                await _messaging.PrintError("Component was destroyed in the meanwhile");
                return;
            }

            // This should be pretty much impossible to happen, but let's handle it anyways
            if(CurrentComponent.Members == null)
            {
                await _messaging.PrintError("Component has no members");
                return;
            }

            foreach(var member in CurrentComponent.Members)
                await PrintMember(member.Value, member.Key);
        }

        async Task PrintMember(Member member, string name, int indentLevel = 0)
        {
            await _messaging.Print($"{name} ({member.ID}): ".PadLeft(indentLevel, ' '));

            switch (member)
            {
                case Field field:
                    if (field.BoxedValue is null)
                        await _messaging.PrintLine("null");
                    else
                        await _messaging.PrintLine(System.Text.Json.JsonSerializer.Serialize(field.BoxedValue, new JsonSerializerOptions()
                        {
                            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
                            // Since most of these are just fields, keep it compact
                            WriteIndented = false
                        }));
                    break;

                case Reference reference:
                    await _messaging.Print($"Target ({reference.TargetType}): ");

                    if (reference.TargetID == null)
                        await _messaging.PrintLine("null");
                    else
                        await _messaging.PrintLine(reference.TargetID);
                    break;

                case EmptyElement emptyElement:
                    await _messaging.PrintLine("<empty element>");
                    break;

                case SyncList list:
                    await _messaging.PrintLine($"<List (Count: {list.Elements?.Count ?? 0})>");

                    if (list.Elements != null)
                        for (int i = 0; i < list.Elements.Count; i++)
                            await PrintMember(list.Elements[i], $"[{i}]", indentLevel + 1);
                    break;

                case SyncObject syncObject:
                    await _messaging.PrintLine("<Object>");

                    foreach (var subMember in syncObject.Members)
                        await PrintMember(subMember.Value, subMember.Key, indentLevel + 1);
                    break;

                default:
                    await _messaging.PrintLine("Unsupported member type: " + member.GetType().Name);
                    break;
            }
        }

        async Task SetMember(string name, string value)
        {
            if (CurrentComponent == null)
                throw new InvalidOperationException("No component is currently selected");

            if(!CurrentComponent.Members.TryGetValue(name, out var member))
            {
                await _messaging.PrintLine($"Member '{name}' doesn't exist");
                return;
            }

            Member setData;

            switch(member)
            {
                case Field_Enum fieldEnum:
                    // A bit special handling for this, because we don't have the actual types
                    // so we just passthrough the value
                    setData = new Field_Enum() { Value = value };
                    break;

                case Field field:
                    try
                    {
                        // We just use JSON to simplify the parsing. This could be swapped out for "nicer"
                        // serialization and parsing, but this will do for purposes of example
                        var setField = (Field)Activator.CreateInstance(member.GetType());
                        setField.BoxedValue = System.Text.Json.JsonSerializer.Deserialize(value, field.ValueType);

                        setData = setField;
                    }
                    catch(Exception ex)
                    {
                        await _messaging.PrintError($"Failed to parse value: {ex.Message}");
                        return;
                    }
                    break;

                case Reference reference:
                    setData = new Reference() { TargetID = value.Trim() };
                    break;

                default:
                    await _messaging.PrintError($"Setting members of type {GetType().Name} is not supported");
                    return;
            }

            setData.ID = member.ID;

            var setComponent = new Component();
            setComponent.ID = CurrentComponent.ID;

            setComponent.Members = new Dictionary<string, Member>();
            setComponent.Members.Add(name, setData);

            var result = await _link.UpdateComponent(new UpdateComponent()
            {
                Data = setComponent
            });

            if (!result.Success)
                await _messaging.PrintError($"Error: " + result.ErrorInfo);
        }

        async Task RemoveComponent(string componentId)
        {
            var result = await _link.RemoveComponent(new RemoveComponent()
            {
                ComponentID = componentId
            });

            if (!result.Success)
                await _messaging.PrintError($"Failed to remove component: {result.ErrorInfo}");
            else if (CurrentComponent?.ID == componentId)
                CurrentComponent = null;
        }

        async Task ImportTexture(string path)
        {
            var result = await _link.ImportTexture(new ImportTexture2DFile()
            {
                FilePath = path
            });

            if (result.Success)
                await _messaging.PrintLine($"URL: {result.AssetURL}");
            else
                await _messaging.PrintError($"Import failed: {result.ErrorInfo}");
        }

        static void SplitCommand(string command, out string keyword, out string arguments)
        {
            command = command.Trim();

            var spaceIndex = command.IndexOf(' ');

            if (spaceIndex < 0)
            {
                keyword = command;
                arguments = null;
            }
            else
            {
                keyword = command.Substring(0, spaceIndex);
                arguments = command.Substring(spaceIndex + 1).Trim();
            }
        }
    }
}
