using Xunit;
using ResoniteLink;
using ResoniteLink.REPL;
using System.Data;

namespace ResoniteLink.Tests.REPL
{
    public class ConsoleCommandIOTests : IDisposable
    {
        private readonly ICommandIO _commandIO = new ConsoleCommandIO();
        private readonly TextReader _originalIn;
        private readonly TextWriter _originalOut;

        public ConsoleCommandIOTests()
        {
            // Preserve original console streams
            _originalIn = Console.In;
            _originalOut = Console.Out;
        }

        public void Dispose()
        {
            // Restore original console streams
            Console.SetIn(_originalIn);
            Console.SetOut(_originalOut);
        }

        [Fact]
        public async Task Print()
        {
            using var outputWriter = new StringWriter();
            Console.SetOut(outputWriter);

            await _commandIO.Print("test print");

            Assert.Equal("test print", outputWriter.ToString());
        }

        [Fact]
        public async Task PrintError()
        {
            using var outputWriter = new StringWriter();
            Console.SetOut(outputWriter);

            await _commandIO.PrintError("error message");

            Assert.Contains("error message", outputWriter.ToString());
        }

        [Fact]
        public async Task PrintLine()
        {
            using var outputWriter = new StringWriter();
            Console.SetOut(outputWriter);

            await _commandIO.PrintLine("test print line");

            Assert.Equal("test print line" + Environment.NewLine, outputWriter.ToString());
        }

        [Fact]
        public async Task PrintPrompt()
        {
            using var outputWriter = new StringWriter();
            Console.SetOut(outputWriter);

            await _commandIO.PrintPrompt("Enter command");

            Assert.Equal("Enter command: ", outputWriter.ToString());
        }

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
        public async Task ReadCommand_Parameterized(string input, CommandType expectedCommand, string expectedArguments)
        {
            // Redirect console input
            using var inputReader = new StringReader(input + Environment.NewLine);
            Console.SetIn(inputReader);

            var (commandType, arguments) = await _commandIO.ReadCommand();

            Assert.Equal(expectedCommand, commandType);
            Assert.Equal(expectedArguments, arguments);
        }

        [Theory]
        [InlineData("garbageCommand")]
        [InlineData("garbageCommand unused arguments")]
        public async Task ReadCommand_UnknownCommand(string input)
        {
            // Redirect console input
            using var inputReader = new StringReader(input + Environment.NewLine);
            Console.SetIn(inputReader);

            // Redirect console output
            using var outputWriter = new StringWriter();
            Console.SetOut(outputWriter);

            var (commandType, arguments) = await _commandIO.ReadCommand();

            Assert.Equal(CommandType.Unknown, commandType);
            Assert.Equal(string.Empty, arguments);
            Assert.Contains($"Unknown command: {input.Split(' ')[0]}", outputWriter.ToString());
        }

        [Theory]
        [InlineData("command", "command", null)]
        [InlineData("command arguments go here", "command", "arguments go here")]
        public void SplitCommand_Parameterized(string input, string expectedKeyword, string expectedArguments)
        {
            _commandIO.SplitCommand(input, out var keyword, out var arguments);

            Assert.Equal(expectedKeyword, keyword);
            Assert.Equal(expectedArguments, arguments);
        }

    }
}