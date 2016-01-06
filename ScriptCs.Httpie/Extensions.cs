using System.Linq;
using RestSharp;
using ScriptCs.Httpie.Streams;

namespace ScriptCs.Httpie
{
    public static class OutputExtensions
    {

        public static void WriteToHost(this IRestResponse response, InputOutput io)
        {
            if (response == null)
            {
                io.Error.WriteLine("No response available.");
                return;
            }

            // TODO: Add ProtocolVersion when it's available

            io.Output.SetColor((int)response.StatusCode > 299 ? Color.Red : Color.DarkGreen);
            io.Output.WriteLine("{0} {1}", (int)response.StatusCode, response.StatusDescription);
            io.Output.ResetColor();

            foreach (var item in response.Headers.OrderBy(header => header.Name))
            {
                io.Output.SetColor(Color.Blue);
                io.Output.Write(item.Name);
                io.Output.SetColor(Color.Green);
                io.Output.Write(":");
                io.Output.SetColor(Color.Magenta);
                io.Output.WriteLine(item.Value);
                io.Output.ResetColor();
            }

            io.Output.WriteLine();
            io.Output.Write(response.RawBytes);
            io.Output.WriteLine();
        }

        public static void WriteToHost(this IRestRequest request, InputOutput io)
        {
            if (request == null)
            {
                io.Error.WriteLine("No request given.");
            }

            io.Output.WriteLine(request.Method);
            io.Output.WriteLine(request.Resource);
            foreach (var parameter in request.Parameters)
            {
                io.Output.WriteLine("Parameter type: {0}", parameter.Type);
                io.Output.WriteLine(parameter);
            }
        }
    }
}
