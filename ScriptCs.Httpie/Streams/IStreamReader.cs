using System;

namespace ScriptCs.Httpie.Streams
{
    public interface IStreamReader : IDisposable
    {
        byte[] ReadUntil(char it);
        byte[] ReadToEnd();
        string ReadLine();
    }
}
