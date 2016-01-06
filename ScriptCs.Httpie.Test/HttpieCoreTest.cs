using System.Collections.Generic;
using System.IO;
using Moq;
using RestSharp;
using Xunit;
using System.Text;

namespace ScriptCs.Httpie.Test
{
    public class HttpieCoreTest
    {
        private Mock<IRestClient> client;
        private Mock<IRestRequest> request;

        private InputOutput io;

        public HttpieCoreTest()
        {
            client = new Mock<IRestClient>();
            request = new Mock<IRestRequest>();
            io = new InputOutput(null, null, null);

            client.SetupProperty(c => c.BaseUrl);
            client.Setup(c => c.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    RawBytes = Encoding.UTF8.GetBytes("Body"),
                    StatusDescription = "OK"
                });
            request.SetupProperty(req => req.Method);
            request.SetupProperty(req => req.Resource);
        }

        [Fact]
        public void SetsOutputToStream()
        {
            using (Stream stream = new MemoryStream())
            {
                using (var httpie = new Httpie(client.Object, request.Object, io).SetOutput(stream))
                {
                    httpie.Get("httpbin.org");
                }

                var result = GetStringContent(stream);
                Assert.Equal("200 OK\r\n\r\nBody\r\n", result);
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
