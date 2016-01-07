using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScriptCs.Httpie.Test
{
    public static class StreamExtensions
    {
        public static string GetStringContent(this Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var bytes = new List<byte>();

            int current = 0;
            current = stream.ReadByte();
            while (current != -1)
            {
                bytes.Add((byte)current);
                current = stream.ReadByte();
            }

            return Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}
