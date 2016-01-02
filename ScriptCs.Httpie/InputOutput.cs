using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Httpie
{
    public class InputOutput
    {
        public TextReader Input { get; private set; }
        public TextWriter Output { get; private set; }
        public TextWriter Error { get; private set; }

        private bool usingConsole;

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

            usingConsole = true;
        }

        public void SetInput(TextReader input) => Input = input;
        public void SetOutput(TextWriter output)
        {
            Output = output;
            usingConsole = false;
        }
        public void SetError(TextWriter error)
        {
            Error = error;
            usingConsole = false;
        }

        public void ChangeColor(ConsoleColor foreground) 
        {
            if (usingConsole)
            {
                Console.ForegroundColor = foreground;
            }
        }

        public void ChangeColor(ConsoleColor foreground, ConsoleColor background) 
        {
            if (usingConsole)
            {
                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;
            }
        }

        public void ResetColor()
        {
            if (usingConsole)
            {
                Console.ResetColor();
            }
        }

        

    }
}
