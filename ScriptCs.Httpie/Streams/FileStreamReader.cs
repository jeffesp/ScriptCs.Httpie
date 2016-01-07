using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScriptCs.Httpie.Streams
{
    public class FileStreamReader : IStreamReader
    {
        private readonly StreamReader input;

        public FileStreamReader(string fileName)
        {
            input = new StreamReader(fileName);
        }

        public byte[] ReadUntil(char it)
        {
            var chars = new List<char>();
            char c;
            do
            {
                c = (char)input.Read();
                chars.Add(c);
            }
            while (c != it);
            return Encoding.UTF8.GetBytes(chars.ToArray());
        }

        public byte[] ReadToEnd()
        {
            return ReadUntil((char)0x13); // EOF
        }

        public string ReadLine()
        {
            return input.ReadLine();
        }

        public void Dispose()
        {
            input.Dispose();
        }
    }
}
