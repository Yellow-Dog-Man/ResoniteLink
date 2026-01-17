namespace ResoniteLink
{

/// <summary>
/// Represents a command with its type and arguments, used for command parsing and execution.
/// </summary>
/// <remarks>
/// This class encapsulates a command's type and its associated arguments, providing a structured way to
/// handle commands within ResoniteLink. The fields are explicitly read-only to ensure immutability after creation.
/// </remarks>
    public class Command
    {
        public CommandType CommandType { get; }
        public string Arguments { get; }

        public Command(CommandType commandType, string arguments)
        {
            CommandType = commandType;
            Arguments = arguments;
        }
    }
}