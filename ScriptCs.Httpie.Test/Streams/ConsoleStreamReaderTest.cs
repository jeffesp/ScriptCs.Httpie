using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ScriptCs.Httpie.Test.Streams
{
    public class ConsoleStreamReaderTest
    {
        private readonly TextReader stream; 
        private readonly TextReader baseOutput;

        private const string testStringCreed = 
    @"This is my test string. There are many like it, but this one is mine.

    My test string is my best friend. It is my life. I must master it as I must
    master my life.

    My test string, without me, is useless. Without my test string, I am
    useless. I must declare my test string with the correct syntax. I must test
    first, rather than my enemy who is testing after. I must refactor only when
    I have a passing test in place.

    My test string and I know that what counts in code is not the SLOC we
    write, the number of unit tests, nor the tooling generated code coverage
    percentage. We know that it is correct execution of our code that counts. 

    My test string is human, even as I, because it is my life. Thus, I will
    learn it as a brother. I will learn its weaknesses, its strength, its
    declaration syntax, its character encoding, its termination character (if
    any), and its mutability. I will keep my test string clean and ready, 
    even as I am clean and ready. We will become part of each other. 

    Before God, I swear this creed. My test string and I are the defenders of
    my code. We are the masters of code correctness. We are the saviors of my
    software quality.

    So be it, until the test run is complete and there is no red, but only
    green!";

        public ConsoleStreamReaderTest()
        {
            baseOutput = Console.In;
            stream = new StringReader(testStringCreed);
            Console.SetIn(stream);
        }

        ~ConsoleStreamReaderTest()
        {
            stream.Dispose();
            Console.SetIn(baseOutput);
        }

        [Fact]
        public void ReadsLineFromConsole()
        {
            using (var target = new ConsoleStreamReader())
            {
                Assert.Equal(testStringCreed.Substring(0, testStringCreed.IndexOf(Environment.NewLine)), target.ReadLine());
            }
        }

        [Fact]
        public void ReadUntilSpecificCharacter()
        {
            using (var target = new ConsoleStreamReader())
            {
                Assert.Equal(Encoding.UTF8.GetBytes(testStringCreed.Substring(0, testStringCreed.IndexOf("f"))), target.ReadUntil('f'));
            }
        }
    }
}
