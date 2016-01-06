using System;
using System.Text;
using ScriptCs.Httpie.Streams;
using Xunit;
namespace ScriptCs.Httpie.Test.Streams
{
    public class ConsoleStreamWriterTest
    {

        [Fact]
        public void WillTruncateByteArrayAndOutputMessage()
        {
            using (IStreamWriter writer = new ConsoleStreamWriter())
            {
                var bytes = Encoding.UTF8.GetBytes(
                    "This is a test that should fill up more than 100 bytes when it is encoded into a byte array. I think this should do it at this point.");

                writer.Write(bytes);
                // I have no idea how to make sure the console actually gets this output unless we put something in front of it solely for mocking purposes.
            }
        }
    }
}
