using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCs.Httpie.Streams;
using Xunit;

namespace ScriptCs.Httpie.Test.Streams
{
    public class StreamStreamWriterTest
    {
        [Fact]
        public void ClosesStreamOnDisposeWhenToldTo()
        {
            using (var stream = new MemoryStream())
            {
                using (var target = new StreamStreamWriter(stream, false))
                {
                    target.Write("test");
                }

                try
                {
                    stream.WriteByte(1);
                    Assert.False(true, "Stream should be closed by the Dispose call on StreamStreamWriter()");
                }
                catch (ObjectDisposedException)
                {
                    
                }
            }
        }

        [Fact]
        public void LeavesStreamOpenOnDisposeWhenToldTo()
        {
            using (var stream = new MemoryStream())
            {
                using (var target = new StreamStreamWriter(stream, true))
                {
                    target.Write("test");
                }

                try
                {
                    stream.WriteByte(1);
                }
                catch (ObjectDisposedException)
                {
                    Assert.False(true, "Stream not be closed by the Dispose call on StreamStreamWriter()");
                }
            }
        }

        [Fact]
        public void LeavesStreamOpenOnDisposeByDefault()
        {
            using (var stream = new MemoryStream())
            {
                using (var target = new StreamStreamWriter(stream))
                {
                    target.Write("test");
                }

                try
                {
                    stream.WriteByte(1);
                }
                catch (ObjectDisposedException)
                {
                    Assert.False(true, "Stream not be closed by the Dispose call on StreamStreamWriter()");
                }
            }
        }

        [Fact]
        public void WritesUTF8EncodedOutputToStream()
        {
            using (var stream = new MemoryStream())
            {
                using (var target = new StreamStreamWriter(stream))
                {
                    target.Write("testöß");
                }

                Assert.Equal("testöß", GetStringContent(stream));
            }
        }

        private string GetStringContent(Stream stream)
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
