using System;
using System.Collections.Generic;
using System.Text;

namespace ResoniteLink.REPL
{
    public class ConsoleCommandIO : ICommandIO
    {
        public async Task Print(string message) => Console.Write(message);

        public async Task PrintError(string message)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ForegroundColor = prevColor;
        }

        public async Task PrintLine(string message) => Console.WriteLine(message);

        public async Task PrintPrompt(string prompt)
        {
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write(prompt + ": ");

            Console.ForegroundColor = prevColor;
        }

        public async Task<(CommandType, string)> ReadCommand()
        {
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                    return (CommandType.Unknown, string.Empty);

            SplitCommand(input, out var keyword, out var arguments);

            try
            {
                // Parse the command from the input string, ignoring case
                CommandType commandType = Enum.Parse<CommandType>(keyword, true);
                return (commandType, arguments ?? string.Empty);

            }
            catch
            {
                await PrintError($"Unknown command: {keyword}");
            }
            return (CommandType.Unknown, string.Empty);
        }

        public void SplitCommand(string command, out string keyword, out string? arguments)
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
