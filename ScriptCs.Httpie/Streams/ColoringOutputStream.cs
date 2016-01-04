using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Httpie.Streams
{
    public class ConsoleStream : IStreamWriter
    {
        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void SetColor(Color foreground)
        {
            Console.ForegroundColor = (ConsoleColor)foreground;
        }

        public void SetColor(Color foreground, Color background)
        {
            Console.ForegroundColor = (ConsoleColor)foreground;
            Console.BackgroundColor = (ConsoleColor)background;
        }

        public void Write(string output)
        {
            Console.Write(output);
        }

        public void Write(string format, object arg0)
        {
            Console.Write(format, arg0);
        }

        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

        public void WriteLine(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
        }

        public void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

    }
}
