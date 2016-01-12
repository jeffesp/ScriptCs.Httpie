using System;
using System.Net.Mime;
using System.Text;

namespace ScriptCs.Httpie.Streams
{
    public class ContentAwareOutputStream : IStreamWriter
    {
        private readonly IStreamWriter baseStream;
        private readonly ContentType contentType;

        public ContentAwareOutputStream(IStreamWriter baseStream, ContentType contentType)
        {
            this.baseStream = baseStream;
            this.contentType = contentType;
        }

        public void ResetColor()
        {
            baseStream.ResetColor();
        }

        public void SetColor(Color foreground)
        {
            baseStream.SetColor(foreground);
        }

        public void SetColor(Color foreground, Color background)
        {
            baseStream.SetColor(foreground, background);
        }

        public void Write(byte[] data)
        {
            // If we think we can write this convert to string before passing it on.
            // TODO: based on type see about formatting it
            if (ContentTypeShouldOutputToConsole(contentType))
            {
                baseStream.Write(Encoding.UTF8.GetString(data));
            }
            else
            {
                baseStream.Write(data);
            }
        }

        public void Write(string output)
        {
            baseStream.Write(output);
        }

        public void Write(string format, object arg0)
        {
            baseStream.Write(format, arg0);
        }

        public void Write(string format, params object[] args)
        {
            baseStream.Write(format, args);
        }

        public void WriteLine()
        {
            baseStream.WriteLine();
        }

        public void WriteLine(object output)
        {
            baseStream.WriteLine();
        }

        public void WriteLine(string output)
        {
            baseStream.WriteLine(output);
        }

        public void WriteLine(string format, object arg0)
        {
            baseStream.WriteLine(format, arg0);
        }

        public void WriteLine(string format, params object[] args)
        {
            baseStream.WriteLine(format, args);
        }

        public bool ContentTypeShouldOutputToConsole(ContentType type)
        {
            return type.MediaType.StartsWith("text/", StringComparison.OrdinalIgnoreCase) 
                || type.MediaType.EndsWith("/json", StringComparison.OrdinalIgnoreCase) 
                || type.MediaType.EndsWith("/xml", StringComparison.OrdinalIgnoreCase);
        }

        public void Flush()
        {
            baseStream.Flush();
        }

        public void Dispose()
        {
        }
    }
}
