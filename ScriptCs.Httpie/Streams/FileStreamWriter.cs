using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Httpie.Streams
{
    public class FileStreamWriter : IStreamWriter, IDisposable
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

        public void Write(string output)
        {
            throw new NotImplementedException();
        }

        public void Write(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Write(string format, object arg0)
        {
            throw new NotImplementedException();
        }

        public void WriteLine()
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string output)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string format, object arg0)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            ((IDisposable)baseStream).Dispose();
        }
    }
}
