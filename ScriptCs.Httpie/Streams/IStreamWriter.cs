using System;

namespace ScriptCs.Httpie.Streams
{
    public interface IStreamWriter : IDisposable
    {
        void SetColor(Color foreground);
        void SetColor(Color foreground, Color background);
        void ResetColor();
        void Write(byte[] data);
        void Write(string output);
        void Write(string format, object arg0);
        void Write(string format, params object[] args);
        void WriteLine();
        void WriteLine(object output);
        void WriteLine(string output);
        void WriteLine(string format, object arg0);
        void WriteLine(string format, params object[] args);
        void Flush();
    }
}