using System;
using System.IO;
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
                    Assert.True(true);
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
            using (var stream = new MemoryStream())
            {
                using (var target = new StreamStreamWriter(stream))
                {
                    target.Write("test");
                }

                try
                {
                    stream.WriteByte(1);
                    Assert.True(true);
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

                Assert.Equal("testöß", stream.GetStringContent());
            }
        }

    }
}
