using System;
using System.Text;

namespace ScriptCs.Httpie.Streams
{
    public class ConsoleStreamWriter : IStreamWriter
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
        public void Write(byte[] data)
        {
            Console.Write(Encoding.UTF8.GetString(data, 0, 100));

            Console.WriteLine();
            Console.WriteLine("Binary data truncated in console output.");
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

        public void Dispose()
        {
        }
    }
}
