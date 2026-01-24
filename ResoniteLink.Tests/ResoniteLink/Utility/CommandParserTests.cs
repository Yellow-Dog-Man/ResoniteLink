using Xunit;
using ResoniteLink;

namespace ResoniteLink.Tests.ResoniteLink
{
    public class CommandParserTests
    {
        [Theory]
        [InlineData("addChild ChildSlot", CommandType.AddChild, "ChildSlot")]
        [InlineData("addComponent MeshRenderer", CommandType.AddComponent, "MeshRenderer")]
        [InlineData("clearComponent", CommandType.ClearComponent, "")]
        [InlineData("componentState", CommandType.ComponentState, "")]
        [InlineData("echo Hello, World!", CommandType.Echo, "Hello, World!")]
        [InlineData("exit", CommandType.Exit, "")]
        [InlineData("importTexture /path/to/texture.png", CommandType.ImportTexture, "/path/to/texture.png")]
        [InlineData("listChildren", CommandType.ListChildren, "")]
        [InlineData("listComponents", CommandType.ListComponents, "")]
        [InlineData("removeComponent", CommandType.RemoveComponent, "")]
        [InlineData("removeSlot", CommandType.RemoveSlot, "")]
        [InlineData("selectChild 0", CommandType.SelectChild, "0")]
        [InlineData("selectComponent 1", CommandType.SelectComponent, "1")]
        [InlineData("selectParent", CommandType.SelectParent, "")]
        [InlineData("setMember position 0,1,0", CommandType.SetMember, "position 0,1,0")]
        [InlineData("garbageCommand", CommandType.Unknown, "garbageCommand")]
        [InlineData("garbageCommand unused arguments", CommandType.Unknown, "garbageCommand")]
        [InlineData("", CommandType.Unknown, "")]
        [InlineData("   ", CommandType.Unknown, "")]
        [InlineData("eChO test ignore case", CommandType.Echo, "test ignore case")]
        public async Task ReadCommand_Parameterized(string input, CommandType expectedKeyword, string expectedArguments)
        {
            Command command = CommandParser.ReadCommand(input);

            Assert.Equal(expectedKeyword, command.CommandType);
            Assert.Equal(expectedArguments, command.Arguments);
        }

        [Theory]
        [InlineData("command", "command", "")]
        [InlineData("command argument", "command", "argument")]
        [InlineData("command arguments go here", "command", "arguments go here")]
        [InlineData("   command    spaced arguments   ", "command", "spaced arguments")]
        public void SplitCommand_Parameterized(string input, string expectedKeyword, string expectedArguments)
        {
            CommandParser.SplitCommand(input, out var keyword, out var arguments);

            Assert.Equal(expectedKeyword, keyword);
            Assert.Equal(expectedArguments, arguments);
        }
    }
}