using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScriptCs.Httpie.Test
{
    public class LiberalUrlParserTest
    {

        [Fact]
        public void ParseStringUrl_AddsHttpIfNotPresent()
        {
            var result = LiberalUrlParser.ParseUrl("example.com");
            Assert.Equal("http://example.com", result);
        }

        [Fact]
        public void ParseStringUrl_DoesNotModifyHttpIfPresent()
        {
            var result = LiberalUrlParser.ParseUrl("http://example.com");
            Assert.Equal("http://example.com", result);
        }

        [Fact]
        public void ParseStringUrl_DoesNotModifyHttpsIfPresent()
        {
            var result = LiberalUrlParser.ParseUrl("https://example.com");
            Assert.Equal("https://example.com", result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void ParseUrl_FailsWithInvalidInput(string input)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                LiberalUrlParser.ParseUrl(input);
            });
        }
    }
}
