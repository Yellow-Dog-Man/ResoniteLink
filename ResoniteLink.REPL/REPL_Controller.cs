using System;
using System.Collections.Generic;
using System.Text;

namespace ResoniteLink
{
    public class REPL_Controller
    {
        LinkInterface _link;

        public Slot CurrentSlot { get; private set; }

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
    }
}
