using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScriptCs.Httpie.Streams
{
    public class StreamStreamReader : IStreamReader
    {
        private readonly Stream stream;
        private readonly bool leaveOpen;

        public StreamStreamReader(Stream stream) : this(stream, true) { }
        public StreamStreamReader(Stream stream, bool leaveOpen)
        {
            this.stream = stream;
            this.leaveOpen = leaveOpen;
        }

        public byte[] ReadToEnd()
        {
            return ReadUntil((char)0x13);
        }

        public byte[] ReadUntil(char it)
        {
            int value = Convert.ToInt32(it);
            if (value > byte.MaxValue)
                throw new InvalidOperationException("");

            var data = new List<byte>();
            byte current = (byte)stream.ReadByte();
            while (current != value)
            {
                data.Add(current);
                current = (byte)stream.ReadByte();
            }
            return data.ToArray();
        }

        public string ReadLine()
        {
            return Encoding.UTF8.GetString(ReadUntil('\n'));
        }

        public void Dispose()
        {
            if (!leaveOpen)
                stream.Dispose();
        }

    }
}
