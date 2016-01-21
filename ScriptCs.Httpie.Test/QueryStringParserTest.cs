using Xunit;

namespace ScriptCs.Httpie.Test
{
    public class QueryStringParserTest
    {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void EmptyValuesReturnEmptyResult(string query)
        {
            var result = QueryStringParser.ParseQueryString(query);
            Assert.Empty(result);
        }

        [Fact]
        public void SingleEntryParsed()
        {
            var result = QueryStringParser.ParseQueryString("foo=bar");
            Assert.NotEmpty(result);
            Assert.Equal("foo", result.Keys[0]);
            Assert.Equal("bar", result[0]);
        }

        [Fact]
        public void MultipleEntryParsed()
        {
            var result = QueryStringParser.ParseQueryString("foo=bar&baz=bin");

            Assert.NotEmpty(result);

            Assert.Equal("foo", result.Keys[0]);
            Assert.Equal("bar", result[0]);

            Assert.Equal("baz", result.Keys[1]);
            Assert.Equal("bin", result[1]);
        }

        [Fact]
        public void HalfGivenEntry_GivesEmptyStringValue()
        {
            var result = QueryStringParser.ParseQueryString("foo=&bar=baz");

            Assert.NotEmpty(result);

            Assert.Equal("foo", result.Keys[0]);
            Assert.Equal("", result[0]);

            Assert.Equal("bar", result.Keys[1]);
            Assert.Equal("baz", result[1]);
        }

        [Fact]
        public void SkipsFirstCharIfQuestionMark()
        {
            var result = QueryStringParser.ParseQueryString("?foo=&bar=baz");

            Assert.NotEmpty(result);
            Assert.Equal("foo", result.Keys[0]);
        }

        [Fact]
        public void HandlesUrlEncodedKeysAndValues()
        {
            var result = QueryStringParser.ParseQueryString("?value=var+http+%3D+Require%3CHttpie%3E()%3B");

            Assert.NotEmpty(result);
            Assert.Equal("var http = Require<Httpie>();", result[0]);
        }
    }
}
