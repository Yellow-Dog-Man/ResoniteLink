using System;
using System.Threading.Tasks;

namespace ResoniteLink
{
    public class CommandParser
    {
        /// <summary>
        /// Reads a command from the console input
        /// </summary>
        /// <returns>
        /// The command read from the input, as the Command enum value and any arguments as a string.
        /// If the command is unknown, CommandType.Unknown will be returned with the keyword as the arguments.
        /// If the input is empty or whitespace, CommandType.Unknown with empty arguments will be returned.
        /// </returns>
        public static Command ReadCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                    return new Command(CommandType.Unknown, string.Empty);

            SplitCommand(input, out var keyword, out var arguments);

            CommandType commandType;

            // Parse the command from the input string, ignoring case
            if (Enum.TryParse<CommandType>(keyword, true, out commandType))
            {
                return new Command(commandType, arguments ?? string.Empty);
            }
            return new Command(CommandType.Unknown, keyword ?? string.Empty);
        }

        /// <summary>
        /// Splits a command string into keyword and arguments, can also be used to split arguments from values as well
        /// </summary>
        /// <param name="command">The full command string to split, or the string to split into keyword and arguments</param>
        /// <param name="keyword">The first word in the command string</param>
        /// <param name="arguments">Any subsequent words in the command string, or an empty string if none</param>
        public static void SplitCommand(string command, out string keyword, out string arguments)
        {
            command = command.Trim();

            var spaceIndex = command.IndexOf(' ');

            if (spaceIndex < 0)
            {
                keyword = command;
                arguments = string.Empty;
            }
            else
            {
                keyword = command.Substring(0, spaceIndex);
                arguments = command.Substring(spaceIndex + 1).Trim();
            }
        }
    }
}