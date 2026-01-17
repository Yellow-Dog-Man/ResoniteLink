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

        public async Task<string> ReadCommand() => Console.ReadLine();
    }
}
