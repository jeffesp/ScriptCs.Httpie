using System;
using System.IO;

namespace ScriptCs.Httpie
{
    /// <summary>
    /// Provides an abstraction around input/output. Provides a way to have output with color or not based 
    /// on if it is using the console as the output. Knows about writing to the console or another destination 
    /// only from constuction or when ResetToConsole() is called.
    /// </summary>
    /// <remarks>
    /// Seems to be calling for a factory with different implementations but going down that road didn't look good. 
    /// Maybe another abstraction is relevant.
    /// </remarks>
    public class InputOutput
    {
        public TextReader Input { get; private set; }
        public TextWriter Output { get; private set; }
        public TextWriter Error { get; private set; }
        public bool UsingConsoleOutput { get; private set; }

        public InputOutput()
        {
            ResetToConsole();
        }

        public InputOutput(TextReader input, TextWriter output, TextWriter error)
        {
            Input = input;
            Output = output;
            Error = error;
        }

        public void ResetToConsole()
        {
            Input = Console.In;
            Output = Console.Out;
            Error = Console.Error;

            UsingConsoleOutput = true;
        }

        public void SetInput(TextReader input) => Input = input;
        public void SetOutput(TextWriter output)
        {
            Output = output;
            UsingConsoleOutput = false;
        }
        public void SetError(TextWriter error)
        {
            Error = error;
            UsingConsoleOutput = false;
        }

        public void ChangeColor(ConsoleColor foreground) 
        {
            if (UsingConsoleOutput)
            {
                Console.ForegroundColor = foreground;
            }
        }

        public void ChangeColor(ConsoleColor foreground, ConsoleColor background) 
        {
            if (UsingConsoleOutput)
            {
                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;
            }
        }

        public void ResetColor()
        {
            if (UsingConsoleOutput)
            {
                Console.ResetColor();
            }
        }

        internal void Flush()
        {
            if (Output != null)
                Output.Flush();
            if (Error != null)
                Error.Flush();
        }
    }
}
