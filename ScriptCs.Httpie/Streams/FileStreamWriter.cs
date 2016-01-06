using System;
using System.IO;

namespace ScriptCs.Httpie.Streams
{
    public class FileStreamWriter : IStreamWriter
    {
        private readonly StreamWriter baseStream;
        public FileStreamWriter(string filename)
        {
            baseStream = new StreamWriter(filename);
        }

        public void ResetColor()
        {
        }

        public void SetColor(Color foreground)
        {
        }

        public void SetColor(Color foreground, Color background)
        {
        }

        public void Write(byte[] data)
        {
            baseStream.BaseStream.Write(data, 0, data.Length);
        }

        public void Write(string output)
        {
            baseStream.Write(output);
        }

        public void Write(string format, params object[] args)
        {
            baseStream.Write(format, args);
        }

        public void Write(string format, object arg0)
        {
            baseStream.Write(format, arg0);
        }

        public void WriteLine()
        {
            baseStream.WriteLine();
        }

        public void WriteLine(string output)
        {
            baseStream.WriteLine(output);
        }

        public void WriteLine(string format, params object[] args)
        {
            baseStream.WriteLine(format, args);
        }

        public void WriteLine(string format, object arg0)
        {
            baseStream.WriteLine(format, arg0);
        }

        public void Dispose()
        {
            ((IDisposable)baseStream).Dispose();
        }
    }
}
