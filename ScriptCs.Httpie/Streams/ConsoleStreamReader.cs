using System;
using ScriptCs.Httpie.Streams;
using System.Text;
using System.Collections.Generic;

namespace ScriptCs.Httpie
{
    public class ConsoleStreamReader : IStreamReader
    {
        public byte[] ReadUntil(char it)
        {
            var chars = new List<char>();
            char c = (char)Console.Read();
            while (c != it)
            {
                chars.Add(c);
                c = (char)Console.Read();
            }
            return Encoding.UTF8.GetBytes(chars.ToArray());
        }

        public byte[] ReadToEnd()
        {
            return ReadUntil((char)0x04);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }


        public void Dispose() { }
    }
}