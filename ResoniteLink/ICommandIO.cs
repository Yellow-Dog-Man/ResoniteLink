using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ResoniteLink
{
    /// <summary>
    /// Interface for command input/output handling
    /// </summary>
    /// <remarks>
    /// The goal for this interface is to provide an interface for a text based interaction. Get text input and then push text output out,
    /// without actually having any responsibility for actual parsing and such. Therefore, this interface should not have any knowledge of
    /// parsing commands or command structure, just input and output of text.
    /// </remarks>
    public interface ICommandIO
    {   
        /// <summary>
        /// Reads a command from the implementing input source
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a Command as the result</returns>
        Task<string> ReadCommand();

        /// <summary>
        /// Prints a prompt message
        /// </summary>
        /// <param name="prompt">The prompt message to print</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task PrintPrompt(string prompt);
        
        /// <summary>
        /// Prints a message without a newline
        /// </summary>
        /// <param name="message">The message to print</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task Print(string message);
        
        /// <summary>
        /// Prints a message with a newline
        /// </summary>
        /// <param name="message">The message to print</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task PrintLine(string message);
        
        /// <summary>
        /// Prints an error message
        /// </summary>
        /// <param name="message">The error message to print</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task PrintError(string message);
    }
}
