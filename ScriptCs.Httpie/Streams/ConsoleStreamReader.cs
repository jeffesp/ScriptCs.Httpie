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
            char c;
            do
            {
                c = (char)Console.Read();
                chars.Add(c);
            }
            while (c != it);
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