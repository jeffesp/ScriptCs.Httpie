using System;
using System.IO;
using ScriptCs.Httpie.Streams;
using Xunit;

namespace ScriptCs.Httpie.Test
{
    public class InputOutputTest 
    {
        [Fact]
        public void SetToConsoleInputOutputByDefault()
        {
            var io = new InputOutput();
            Assert.Equal(Console.In, io.Input);
            Assert.IsAssignableFrom<ConsoleStreamWriter>(io.Output);
            Assert.IsAssignableFrom<ConsoleStreamWriter>(io.Error);
        }

        [Fact]
        public void SetInputThenOuputAndErrorNotChanged()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var reader = new StreamReader(ms))
            {
                io.SetInput(reader);
                Assert.IsAssignableFrom<ConsoleStreamWriter>(io.Output);
                Assert.IsAssignableFrom<ConsoleStreamWriter>(io.Error);
                Assert.Equal(reader, io.Input);
            }
        }

        [Fact]
        public void SetOutputThenInputAndErrorNotChanged()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var writer = new StreamStreamWriter(ms))
            {
                io.SetOutput(writer);
                Assert.Equal(writer, io.Output);
                Assert.Equal(Console.In, io.Input);
                Assert.IsAssignableFrom<ConsoleStreamWriter>(io.Error);
            }
        }

        [Fact]
        public void SetErrorThenInputAndOutputNotChanged()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var writer = new StreamStreamWriter(ms))
            {
                io.SetError(writer);
                Assert.IsAssignableFrom<ConsoleStreamWriter>(io.Output);
                Assert.Equal(Console.In, io.Input);
                Assert.Equal(writer, io.Error);
            }
        }


        [Fact(Skip = "Can't change console color in dll and then check in test project?")]
        public void WhenUsingConsoleOutputChangingColorChangesConsoleColor()
        {
            var io = new InputOutput();
            io.Output.SetColor(Color.Cyan, Color.DarkBlue);
            Assert.Equal(ConsoleColor.Cyan, Console.ForegroundColor);
            Assert.Equal(ConsoleColor.DarkBlue, Console.BackgroundColor);
        }
    }
}
