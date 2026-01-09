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
    }
}