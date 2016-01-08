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
            
        }

    }
}
