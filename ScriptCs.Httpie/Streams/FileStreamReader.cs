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
            // this is probably totally inefficient when the underlying stream can read blocks...
            var chars = new List<char>();
            char c = (char)input.Read();
            while (c != it)
            {
                chars.Add(c);
                c = (char)input.Read();
            }
            return Encoding.UTF8.GetBytes(chars.ToArray());
        }

        public byte[] ReadToEnd()
        {
            return ReadUntil((char)0xFFFF); // EOF
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
