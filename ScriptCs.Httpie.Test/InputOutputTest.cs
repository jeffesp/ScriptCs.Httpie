﻿using System;
using System.IO;
using Xunit;

namespace ScriptCs.Httpie.Test
{
    public class InputOutputTest 
    {
        [Fact]
        public void SetToConsoleInputOutputByDefault()
        {
            var io = new InputOutput();
            Assert.Equal(Console.Out, io.Output);
            Assert.Equal(Console.In, io.Input);
            Assert.Equal(Console.Error, io.Error);
        }

        [Fact]
        public void SetInputAndOuputAndErrorNotChanged()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var reader = new StreamReader(ms))
            {
                io.SetInput(reader);
                Assert.Equal(Console.Out, io.Output);
                Assert.Equal(reader, io.Input);
                Assert.Equal(Console.Error, io.Error);
            }
        }

        [Fact]
        public void SetOutputAndInputAndErrorNotChanged()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            {
                io.SetOutput(writer);
                Assert.Equal(writer, io.Output);
                Assert.Equal(Console.In, io.Input);
                Assert.Equal(Console.Error, io.Error);
            }
        }

        [Fact]
        public void SetErrorAndInputAndOutputNotChanged()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            {
                io.SetError(writer);
                Assert.Equal(Console.Out, io.Output);
                Assert.Equal(Console.In, io.Input);
                Assert.Equal(writer, io.Error);
            }
        }

        [Fact]
        public void SettingInputStillUsingConsoleOutput()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var reader = new StreamReader(ms))
            {
                io.SetInput(reader);
                Assert.True(io.UsingConsoleOutput);
            }
        }

        [Fact]
        public void SettingOutputNoLongerUsingConsoleOutput()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            {
                io.SetOutput(writer);
                Assert.False(io.UsingConsoleOutput);
            }
        }

        [Fact]
        public void ResetToConsoleWillUseConsoleOutput()
        {
            var io = new InputOutput();
            using (var ms = new MemoryStream())
            using (var writer = new StreamWriter(ms))
            {
                io.SetOutput(writer);
                io.ResetToConsole();
                Assert.True(io.UsingConsoleOutput);
            }
        }


        [Fact(Skip = "Can't change console color in dll and then check in test project?")]
        public void WhenUsingConsoleOutputChangingColorChangesConsoleColor()
        {
            var io = new InputOutput();
            io.ChangeColor(ConsoleColor.Cyan, ConsoleColor.DarkBlue);
            Assert.Equal(ConsoleColor.Cyan, Console.ForegroundColor);
            Assert.Equal(ConsoleColor.DarkBlue, Console.BackgroundColor);
        }
    }
}