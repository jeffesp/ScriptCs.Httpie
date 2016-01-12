using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Httpie.Streams
{
    public class StreamStreamWriter : IStreamWriter
    {
        private readonly Stream baseStream;
        private readonly bool leaveOpen;

        /// <summary>
        /// Wrap a <c>Stream</c> in the operation needed for this project. Will close the stream on call to <c>Dispose</c>.
        /// </summary>
        /// <param name="baseStream">The stream to write the content to.</param>
        public StreamStreamWriter(Stream baseStream) : this(baseStream, true) { }

        public StreamStreamWriter(Stream baseStream, bool leaveOpen)
        {
            this.baseStream = baseStream;
            this.leaveOpen = leaveOpen;
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
            baseStream.Write(data, 0, data.Length);
        }

        public void Write(string output)
        {
            var data = Encoding.UTF8.GetBytes(output);
            baseStream.Write(data, 0, data.Length);
        }

        public void Write(string format, object arg0)
        {
           Write(string.Format(format, arg0));
        }

        public void Write(string format, params object[] args)
        {
            Write(string.Format(format, args));
        }

        public void WriteLine()
        {
            Write(Environment.NewLine);
        }

        public void WriteLine(object output)
        {
            Write(output + Environment.NewLine);
        }

        public void WriteLine(string output)
        {
            Write(output + Environment.NewLine);
        }

        public void WriteLine(string format, object arg0)
        {
            Write(string.Format(format, arg0) + Environment.NewLine);
        }

        public void WriteLine(string format, params object[] args)
        {
            Write(string.Format(format, args) + Environment.NewLine);
        }

        public void Flush()
        {
            baseStream.Flush();
        }

        public void Dispose()
        {
            if (!leaveOpen)
                ((IDisposable)baseStream).Dispose();
        }
    }
}
