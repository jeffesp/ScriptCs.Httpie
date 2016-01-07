using ScriptCs.Httpie.Streams;

namespace ScriptCs.Httpie
{
    public class InputOutput
    {
        public IStreamReader Input { get; private set; }
        public IStreamWriter Output { get; private set; }
        public IStreamWriter Error { get; private set; }

        public InputOutput()
        {
            ResetToConsole();
        }

        public InputOutput(IStreamReader input, IStreamWriter output, IStreamWriter error)
        {
            Input = input;
            Output = output;
            Error = error;
        }

        public void ResetToConsole()
        {
            Input = new ConsoleStreamReader();
            Output = new ConsoleStreamWriter();
            Error = new ConsoleStreamWriter();
        }

        public void SetInput(IStreamReader input) => Input = input;
        public void SetOutput(IStreamWriter output) => Output = output;
        public void SetError(IStreamWriter error) => Error = error;

        internal void Flush()
        {
            Output?.Flush();
            Error?.Flush();
        }
    }
}
