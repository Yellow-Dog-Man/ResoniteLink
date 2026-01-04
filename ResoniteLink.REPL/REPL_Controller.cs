using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ResoniteLink
{
    public class REPL_Controller
    {
        LinkInterface _link;

        public Slot CurrentSlot { get; private set; }
        public Component CurrentComponent { get; private set; }

        int _idPool;

        public REPL_Controller(LinkInterface link)
        {
            _link = link;
        }

        public async Task RunLoop()
        {
            // Make sure we start with the root slot selected
            if (CurrentSlot == null)
                await SelectSlot(Slot.ROOT_SLOT_ID);

            bool keepProcessing;

            do
            {
                PrintPrompt();

                var command = Console.ReadLine();

                keepProcessing = await ProcessCommand(command);
            } while (keepProcessing);
        }

        void PrintPrompt()
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write($"Slot: {CurrentSlot.Name.Value} (ID: {CurrentSlot.ID})");

            if (CurrentComponent != null)
                Console.Write($" Component: {CurrentComponent.ComponentType} (ID: {CurrentComponent.ID})");

            Console.Write(":");

            Console.ForegroundColor = prevColor;
        }
        
        async Task<bool> ProcessCommand(string command)
        {
            SplitCommand(command, out var keyword, out var arguments);

            // Normalize it, so we are case insensitive
            keyword = keyword.ToLowerInvariant();

            switch(keyword)
            {
                case "echo":
                    // Not necessary, but a good for sanity check
                    Console.WriteLine(arguments);
                    break;

                case "listchildren":
                    await RefreshCurrent();

                    Console.WriteLine("Children count: " + (CurrentSlot.Children?.Count ?? 0));

                    for(int i = 0; i < (CurrentSlot.Children?.Count ?? 0); i++)
                    {
                        var child = CurrentSlot.Children[i];
                        Console.WriteLine($"\t[{i}] {child.Name.Value} (ID: {child.ID})");
                    }

                    break;

                case "listcomponents":
                    Console.WriteLine("Component count: " + (CurrentSlot.Components?.Count ?? 0));

                    for (int i = 0; i < (CurrentSlot.Components?.Count ?? 0); i++)
                    {
                        var component = CurrentSlot.Components[i];
                        Console.WriteLine($"\t[{i}] {component.ComponentType} (ID: {component.ID})");
                    }
                    break;

                case "selectchild":
                    if(!int.TryParse(arguments, out var childIndex))
                    {
                        Console.WriteLine("Could not parse child index");
                        break;
                    }

                    if(childIndex < 0 || childIndex >= (CurrentSlot.Children?.Count ?? 0))
                    {
                        Console.WriteLine("Child Index is out of range");
                        break;
                    }

                    await SelectSlot(CurrentSlot.Children[childIndex].ID);

                    break;

                case "selectcomponent":
                    if (!int.TryParse(arguments, out var componentIndex))
                    {
                        Console.WriteLine("Could not parse component index");
                        break;
                    }

                    if (componentIndex < 0 || componentIndex >= (CurrentSlot.Components?.Count ?? 0))
                    {
                        Console.WriteLine("Component Index is out of range");
                        break;
                    }

                    await SelectComponent(CurrentSlot.Components[componentIndex].ID);
                    break;

                case "clearcomponent":
                    CurrentComponent = null;
                    break;

                case "componentstate":
                    await RefreshCurrent();

                    if (CurrentComponent == null)
                    {
                        Console.WriteLine("No component is selected");
                        break;
                    }

                    PrintComponentMembers();
                    break;

                case "addchild":
                    if(string.IsNullOrWhiteSpace(arguments))
                    {
                        Console.WriteLine("You must provide a name of the child");
                        break;
                    }

                    // Add the child
                    var childId = await AddChild(arguments.Trim());

                    // Immediatelly select the new child
                    if (childId != null)
                        Console.WriteLine($"Child added. ID: {childId}");
                    break;

                case "removeslot":
                    await RemoveCurrentSlot();
                    break;

                case "selectparent":
                    if(CurrentSlot.Parent.TargetID == null)
                    {
                        Console.WriteLine("Root is topmost slot, cannot select parent");
                        break;
                    }

                    await SelectSlot(CurrentSlot.Parent.TargetID);

                    break;

                case "set":
                    if(CurrentComponent == null)
                    {
                        Console.WriteLine("No component is selected");
                        break;
                    }

                    SplitCommand(arguments, out var memberName, out var setValue);

                    if(setValue == null)
                    {
                        Console.WriteLine("Invalid number of arguments. Usage: set <MemberName> <Value as JSON>");
                        break;
                    }

                    await SetMember(memberName, setValue);
                    break;

                case "exit":
                    // We stop processing
                    return false;

                default:
                    Console.WriteLine($"Unknown command: {keyword}");
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
                Console.WriteLine($"Error! Resetting back to root");
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
                Console.WriteLine($"Error! Failed to fetch component data: {result.ErrorInfo}");
                return;
            }

            CurrentComponent = result.Data;

            PrintComponentMembers();
        }

        async Task<string> AddChild(string name)
        {
            // We allocate our own ID, so we can immediatelly select it after without having to fetch it back
            var childId = $"REPL_{_idPool++:X}";

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
                Console.WriteLine($"Failed to add child: " + result.ErrorInfo);
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
                Console.WriteLine($"Failed to remove slot: {result.ErrorInfo}");
                return;
            }

            // Select the parent
            await SelectSlot(CurrentSlot.Parent.TargetID);
        }

        void PrintComponentMembers()
        {
            if (CurrentComponent == null)
                throw new InvalidOperationException("No component is currently selected");

            if(CurrentComponent == null)
            {
                Console.WriteLine("Component was destroyed in the meanwhile");
                return;
            }

            // This should be pretty much impossible to happen, but let's handle it anyways
            if(CurrentComponent.Members == null)
            {
                Console.WriteLine("Component has no members");
                return;
            }

            foreach(var member in CurrentComponent.Members)
                PrintMember(member.Value, member.Key);
        }

        void PrintMember(Member member, string name, int indentLevel = 0)
        {
            Console.Write($"{name} ({member.ID}): ".PadLeft(indentLevel, ' '));

            switch (member)
            {
                case Field field:
                    if (field.BoxedValue is null)
                        Console.WriteLine("null");
                    else
                        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(field.BoxedValue, new JsonSerializerOptions()
                        {
                            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
                            // Since most of these are just fields, keep it compact
                            WriteIndented = false
                        }));
                    break;

                case Reference reference:
                    Console.Write($"Target ({reference.TargetType}): ");

                    if (reference.TargetID == null)
                        Console.WriteLine("null");
                    else
                        Console.WriteLine(reference.TargetID);
                    break;

                case SyncList list:
                    Console.WriteLine($"<List (Count: {list.Elements?.Count ?? 0})>");

                    if (list.Elements != null)
                        for (int i = 0; i < list.Elements.Count; i++)
                            PrintMember(list.Elements[i], $"[{i}]", indentLevel + 1);
                    break;

                case SyncObject syncObject:
                    Console.WriteLine("<Object>");

                    foreach (var subMember in syncObject.Members)
                        PrintMember(subMember.Value, subMember.Key, indentLevel + 1);
                    break;

                default:
                    Console.WriteLine("Unsupported member type: " + member.GetType().Name);
                    break;
            }
        }

        async Task SetMember(string name, string value)
        {
            if (CurrentComponent == null)
                throw new InvalidOperationException("No component is currently selected");

            if(!CurrentComponent.Members.TryGetValue(name, out var member))
            {
                Console.WriteLine($"Member '{name}' doesn't exist");
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
                        Console.WriteLine($"Failed to parse value: {ex.Message}");
                        return;
                    }
                    break;

                case Reference reference:
                    setData = new Reference() { TargetID = value.Trim() };
                    break;

                default:
                    Console.WriteLine($"Setting members of type {GetType().Name} is not supported");
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
                Console.WriteLine($"Error: " + result.ErrorInfo);
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
