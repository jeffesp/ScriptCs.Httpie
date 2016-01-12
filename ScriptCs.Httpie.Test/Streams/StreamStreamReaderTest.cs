using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ScriptCs.Httpie.Streams;
using Xunit;

namespace ScriptCs.Httpie.Test.Streams
{
    public class StreamStreamReaderTest
    {
        [Fact]
        public void ReadLineReadsToASingleNewlineCharacter()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("this is a test\r\n")))
            {
                using (var target = new StreamStreamReader(stream))
                {
                    var result = target.ReadLine();
                    Assert.Equal("this is a test\r", result);
                }
            }
        }

        [Fact]
        public void ReadToEndReadsUntilEOF()
        {
            var bytes = Encoding.UTF8.GetBytes("this is a test\u0013");
            using (var stream = new MemoryStream(bytes))
            {
                using (var target = new StreamStreamReader(stream))
                {
                    var result = target.ReadToEnd();
                    Assert.Equal(bytes.Take(bytes.Length - 1), result);
                }
            }
        }

        [Fact]
        public void ClosesStreamOnDisposeWhenToldTo()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("this is a test\n")))
            {
                using (var target = new StreamStreamReader(stream, false))
                {
                    target.ReadLine();
                }

                try
                {
                    stream.ReadByte();
                    Assert.False(true, "Stream should be closed by the Dispose call on StreamStreamWriter()");
                }
                catch (ObjectDisposedException)
                {
                    Assert.True(true);
                }
            }
        }

        [Fact]
        public void LeavesStreamOpenOnDisposeWhenToldTo()
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("this is a test\n")))
            {
                using (var target = new StreamStreamReader(stream, true))
                {
                    target.ReadLine();
                }

                try
                {
                    stream.ReadByte();
                    Assert.True(true);
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
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("this is a test\n")))
            {
                using (var target = new StreamStreamReader(stream))
                {
                    target.ReadLine();
                }

                try
                {
                    stream.ReadByte();
                    Assert.True(true);
                }
                catch (ObjectDisposedException)
                {
                    Assert.False(true, "Stream not be closed by the Dispose call on StreamStreamWriter()");
                }
            }
        }

    }
}
