using System;
using Xunit;

namespace ScriptCs.Httpie.Test
{
    public class MinimalUrlParserTest
    {

        [Fact]
        public void ParseStringUrl_AddsHttpIfNotPresent()
        {
            var result = MinimalUrlParser.ParseUrl("example.com");
            Assert.Equal("http://example.com", result);
        }

        [Fact]
        public void ParseStringUrl_DoesNotModifyHttpIfPresent()
        {
            var result = MinimalUrlParser.ParseUrl("http://example.com");
            Assert.Equal("http://example.com", result);
        }

        [Fact]
        public void ParseStringUrl_DoesNotModifyHttpsIfPresent()
        {
            var result = MinimalUrlParser.ParseUrl("https://example.com");
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
                MinimalUrlParser.ParseUrl(input);
            });
        }
    }
}
