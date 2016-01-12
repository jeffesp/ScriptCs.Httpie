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
    public class FileStreamWriterTest
    {
        private readonly string tempFileName;

        public FileStreamWriterTest()
        {
            tempFileName = Path.GetTempFileName();
        }

        ~FileStreamWriterTest()
        {
            File.Delete(tempFileName);
        }

        [Fact]
        public void BinaryContentIsWrittenToUnderlyingFile()
        {
            string text = "This is a test.";
            using (var writer = new FileStreamWriter(tempFileName))
            {
                writer.Write(Encoding.UTF8.GetBytes(text)); 
            }

            Assert.Equal(text, File.ReadAllText(tempFileName));
        }
    }
}
