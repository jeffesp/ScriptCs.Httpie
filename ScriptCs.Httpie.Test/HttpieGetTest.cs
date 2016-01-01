using System;
using Moq;
using RestSharp;
using Xunit;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ScriptCs.Httpie.Test
{
    public class HttpieGetTest
    {
        Mock<IRestClient> client;
        Mock<IRestRequest> request;

        public HttpieGetTest()
        {
            client = new Mock<IRestClient>();
            request = new Mock<IRestRequest>();

            client.SetupProperty(c => c.BaseUrl);
            request.SetupProperty(req => req.Method);
            request.SetupProperty(req => req.Resource);
        }

        [Fact]
        public void WillGetByDefault()
        {
            new Httpie(client.Object, request.Object).Url("example.com").Get();

            Assert.Equal(Method.GET, request.Object.Method);
            Assert.Equal(new Uri("http://example.com"), client.Object.BaseUrl);
        }

        [Fact]
        public void SplitsHostAndPathAndQuery()
        {
            new Httpie(client.Object, request.Object).Url("example.com/foo/bar?baz=bin").Get();

            Assert.Equal("foo/bar", request.Object.Resource);
        }

        [Fact]
        public void SplitsQueryStringParameters()
        {
            var parsed = new Dictionary<string,string>();

            request.Setup(req => req.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>())).Callback<string,string>((k, v) => {parsed.Add(k,v);});

            new Httpie(client.Object, request.Object).Url("example.com/foo/bar?baz=bin&quxx=5").Get();

            Assert.Equal(new Dictionary<string,string> { { "baz", "bin" }, { "quxx", "5" } }, parsed);
        }

        [Fact]
        public void AddsPortToUrl()
        {
            new Httpie(client.Object, request.Object).Url("httpbin.org").Port(80).Get();
            
            Assert.Equal(80, client.Object.BaseUrl.Port);
        }

        [Fact]
        public void AppendsQueryToExisting()
        {
            var parsed = new Dictionary<string,string>();
            request.Setup(req => req.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>())).Callback<string,string>((k, v) => {parsed.Add(k,v);});

            new Httpie(client.Object, request.Object).Url("httpbin.org/response-headers").Query("test=value").Get();

            Assert.Equal(new Dictionary<string,string> { { "test", "value" } }, parsed);
        }

        [Fact]
        public void AddsQueryToUrl()
        {
            var parsed = new Dictionary<string,string>();
            request.Setup(req => req.AddQueryParameter(It.IsAny<string>(), It.IsAny<string>())).Callback<string,string>((k, v) => {parsed.Add(k,v);});

            new Httpie(client.Object, request.Object).Url("httpbin.org/response-headers?start=query").Query("test=value").Get();

            Assert.Equal(new Dictionary<string,string> { { "start", "query"}, { "test", "value" } }, parsed);
        }

        [Fact]
        public void CanAddHeaderToRequest()
        {
            var headers = new Dictionary<string,string>();
            request.Setup(req => req.AddHeader(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => { headers.Add(k, v); });
            new Httpie(client.Object, request.Object).Url("httpbin.org").Header("Something", "a-value").Get();
            Assert.Equal(new Dictionary<string, string> {{"Something", "a-value"}}, headers);
        }

        [Fact]
        public void CanAddAcceptHeaderFromString()
        {
            var headers = new Dictionary<string,string>();
            request.Setup(req => req.AddHeader(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => { headers.Add(k, v); });
            new Httpie(client.Object, request.Object).Url("httpbin.org").Accept("abc/def");
            Assert.Equal(new Dictionary<string, string> {{"accept", "abc/def"}}, headers);
        }

        [Fact]
        public void CanAddAcceptHeaderFromContentType()
        {
            var headers = new Dictionary<string,string>();
            request.Setup(req => req.AddHeader(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((k, v) => { headers.Add(k, v); });
            new Httpie(client.Object, request.Object).Url("httpbin.org").Accept(new ContentType("abc/def"));
            Assert.Equal(new Dictionary<string, string> {{"accept", "abc/def"}}, headers);
        }

        [Fact]
        public async Task CanAwaitAsyncGet()
        {
            await new Httpie(client.Object, request.Object).Url("example.com").GetAsync();

            Assert.Equal(Method.GET, request.Object.Method);
            Assert.Equal(new Uri("http://example.com"), client.Object.BaseUrl);
        }
    }
}
