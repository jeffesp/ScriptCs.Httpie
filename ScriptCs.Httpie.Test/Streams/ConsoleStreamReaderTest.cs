using System;
using System.IO;
using System.Text;
using Xunit;

namespace ScriptCs.Httpie.Test.Streams
{
    public class ConsoleStreamReaderTest
    {
        private TextReader stream; 
        private readonly TextReader baseOutput;

        private const string unitTestingCreed = 
    @"This is my unit test. There are many like it, but this one is mine.

    My unit test is my best friend. It is my life. I must master it as I must
    master my life.

    My unit test, without me, is useless. Without my unit test, I am
    useless. I must declare my unit test with the correct syntax. I must test
    first, rather than my enemy who is testing after. I must refactor only when
    I have a passing test in place.

    My unit test and I know that what counts in code is not the SLOC we
    write, the number of unit tests, nor the tooling generated code coverage
    percentage. We know that it is correct execution of our code that counts. 

    My unit test is human, even as I, because it is my life. Thus, I will
    learn it as a brother. I will learn its weaknesses, its strength, its
    assertion syntax, its mocking framework, its test runner support, and its . 
    I will keep my unit test clean and ready, 
    even as I am clean and ready. We will become part of each other. 

    Before God, I swear this creed. My unit test and I are the defenders of
    my code. We are the masters of code correctness. We are the saviors of my
    software quality.

    So be it, until the test run is complete and there is no red, but only
    green!";


        private string stringWithCtrlD = "something here" + '\x0004';

        public ConsoleStreamReaderTest()
        {
            baseOutput = Console.In;
        }

        ~ConsoleStreamReaderTest()
        {
            stream.Dispose();
            Console.SetIn(baseOutput);
        }

        [Fact]
        public void ReadsLineFromConsole()
        {
            stream = new StringReader(unitTestingCreed);
            Console.SetIn(stream);
            using (var target = new ConsoleStreamReader())
            {
                Assert.Equal(unitTestingCreed.Substring(0, unitTestingCreed.IndexOf(Environment.NewLine)), target.ReadLine());
            }
        }

        [Fact]
        public void ReadUntilSpecificCharacter()
        {
            stream = new StringReader(unitTestingCreed);
            Console.SetIn(stream);
            using (var target = new ConsoleStreamReader())
            {
                var expected = Encoding.UTF8.GetBytes(unitTestingCreed.Substring(0, unitTestingCreed.IndexOf("f")));
                var actual = target.ReadUntil('f');
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ReadToEndReadsToEOT()
        {
            stream = new StringReader(stringWithCtrlD);
            Console.SetIn(stream);
            using (var target = new ConsoleStreamReader())
            {
                var expected = Encoding.UTF8.GetBytes(stringWithCtrlD.Substring(0, stringWithCtrlD.Length - 1));
                var actual = target.ReadToEnd();
                Assert.Equal(expected, actual);
            }
        }
    }
}
