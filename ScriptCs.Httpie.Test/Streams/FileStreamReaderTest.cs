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
    public class FileStreamReaderTest
    {
        private readonly string tempFileName;

        public FileStreamReaderTest()
        {
            tempFileName = Path.GetTempFileName();
        }

        ~FileStreamReaderTest()
        {
            File.Delete(tempFileName);
        }

        [Fact]
        public void ReadToEndReadsToEOF()
        {
            string content = "this is some text";
            File.WriteAllText(tempFileName, content);

            using (var stream = new FileStreamReader(tempFileName))
            {
                var result = stream.ReadToEnd();

                Assert.Equal(Encoding.UTF8.GetBytes(content), result);
            }

        }

        [Fact]
        public void ReadUntilWillStopAtCharacter()
        {
            string content = "this is some text";
            File.WriteAllText(tempFileName, content);

            using (var stream = new FileStreamReader(tempFileName))
            {
                var result = stream.ReadUntil('o');

                Assert.Equal(Encoding.UTF8.GetBytes(content.Substring(0, content.IndexOf("o"))), result);
            }
        }

        [Fact]
        public void ReadLineWillReadALineOfText()
        {
            string content = "this is some text" + Environment.NewLine + "Another line";
            File.WriteAllText(tempFileName, content);

            using (var stream = new FileStreamReader(tempFileName))
            {
                var result = stream.ReadLine();

                Assert.Equal(content.Substring(0, content.IndexOf(Environment.NewLine)), result);
            }
        }
    }
}
