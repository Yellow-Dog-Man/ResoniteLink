using System;
using System.Collections.Generic;
using System.Text;

namespace ResoniteLink
{
    public class REPL_Controller
    {
        LinkInterface _link;

        public Slot CurrentSlot { get; private set; }

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
            Console.Write(":");

            Console.ForegroundColor = prevColor;
        }
        
        async Task<bool> ProcessCommand(string command)
        {
            command = command.Trim();

            var spaceIndex = command.IndexOf(' ');

            string keyword, arguments;

            if(spaceIndex < 0)
            {
                keyword = command;
                arguments = null;
            }
            else
            {
                keyword = command.Substring(0, spaceIndex);
                arguments = command.Substring(spaceIndex + 1).Trim();
            }

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

                    if(childIndex < 0 || childIndex >= CurrentSlot.Children.Count)
                    {
                        Console.WriteLine("Child Index is out of range");
                        break;
                    }

                    await SelectSlot(CurrentSlot.Children[childIndex].ID);

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
                    if(childId != null)
                        await SelectSlot(childId);

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
            await SelectSlot(CurrentSlot.Parent.ID);
        }
    }
}
