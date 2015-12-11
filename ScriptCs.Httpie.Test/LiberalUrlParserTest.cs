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
        private LiberalUrlParser target;
        public LiberalUrlParserTest()
        {
            target = new LiberalUrlParser();
        }

        [Fact]
        public void ParseStringUrl_AddsHttpIfNotPresent()
        {
            var result = target.ParseUrl("example.com");
            Assert.Equal("http://example.com", result);
        }

        [Fact]
        public void ParseStringUrl_DoesNotModifyHttpIfPresent()
        {
            var result = target.ParseUrl("http://example.com");
            Assert.Equal("http://example.com", result);
        }

        [Fact]
        public void ParseStringUrl_DoesNotModifyHttpsIfPresent()
        {
            var result = target.ParseUrl("https://example.com");
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
                var target = new LiberalUrlParser();
                target.ParseUrl(input);
            });
        }
    }
}
