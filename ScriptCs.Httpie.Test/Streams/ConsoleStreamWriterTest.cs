using System;
using System.IO;
using System.Text;
using ScriptCs.Httpie.Streams;
using Xunit;

namespace ScriptCs.Httpie.Test.Streams
{
    public class ConsoleStreamWriterTest
    {
        private readonly TextWriter stream; 
        private readonly TextWriter baseOutput;

        public ConsoleStreamWriterTest()
        {
            baseOutput = Console.Out;
            stream = new StringWriter();
            Console.SetOut(stream);
        }

        ~ConsoleStreamWriterTest()
        {
            stream.Dispose();
            Console.SetOut(baseOutput);
        }

        [Fact]
        public void WillTruncateByteArrayAndOutputMessage()
        {
            string content =
                "This is a test that should fill up more than 100 bytes when it is encoded into a byte array. I think this should do it at this point.";
            string expected = content.Substring(0, 100) + "\r\nBinary data truncated in console output.\r\n";
            using (IStreamWriter writer = new ConsoleStreamWriter())
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                writer.Write(bytes);
                writer.Flush();
            }

            Assert.Equal(expected, stream.ToString());
        }
    }
}
