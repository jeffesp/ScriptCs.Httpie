using System.ComponentModel.Composition.Primitives;
using System.Net.Mime;
using Moq;
using ScriptCs.Httpie.Streams;
using Xunit;
using System.Text;

namespace ScriptCs.Httpie.Test.Streams
{
    public class ContentAwareOutputStreamTest
    {
        private readonly Mock<IStreamWriter> baseStream;

        public ContentAwareOutputStreamTest()
        {
            baseStream = new Mock<IStreamWriter>();
        }

        [Fact]
        public void ApplicationSlashJsonTypeWillBeOutputAsString()
        {
            string json = "{ 'key' : 'value', 'key2': 'value2', 'arr': [ 1, 2, 3 ], 'obj' : { 'objkey' : 'objvalue' } }";
            baseStream.Setup(s => s.Write(json));
            using (var target = new ContentAwareOutputStream(baseStream.Object, new ContentType("application/json")))
                target.Write(Encoding.UTF8.GetBytes(json));
        }

        [Fact]
        public void ApplicationSlashXmlIsOutputAsString()
        {
            string xml =
                "<toplevel><key>value</key><key2>value2</key2><arr><item>1</item><item>2</item><item>3</item></arr></toplevel>";
            baseStream.Setup(s => s.Write(xml));
            using (var target = new ContentAwareOutputStream(baseStream.Object, new ContentType("application/xml")))
                target.Write(Encoding.UTF8.GetBytes(xml));
        }

        [Fact]
        public void TextSlashAnythingIsOutputAsString()
        {
            string text = "this is some text content.";
            baseStream.Setup(s => s.Write(text));
            using (var target = new ContentAwareOutputStream(baseStream.Object, new ContentType("text/stuff")))
                target.Write(Encoding.UTF8.GetBytes(text));
        }

        [Fact]
        public void BinaryDataIsOutputAsByteArray()
        {
            byte[] data = Encoding.UTF8.GetBytes("Not actual quicktime video content, but acting as it for this unit test.");
            baseStream.Setup(s => s.Write(data));
            using (var target = new ContentAwareOutputStream(baseStream.Object, new ContentType("video/quicktime")))
                target.Write(data);
        }
    }

}
