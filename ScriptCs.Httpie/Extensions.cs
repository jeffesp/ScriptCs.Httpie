using System;
using System.Linq;
using RestSharp;

namespace ScriptCs.Httpie
{
    public static class Extensions
    {

        public static void WriteToHost(this IRestResponse response, InputOutput io)
        {
            if (response == null)
            {
                io.Error.WriteLine("No response available.");
                return;
            }

            // TODO: Add ProtocolVersion when it's available

            io.ChangeColor((int)response.StatusCode > 299 ? ConsoleColor.Red : ConsoleColor.DarkGreen);
            io.Output.WriteLine("{0} {1}", (int)response.StatusCode, response.StatusDescription);
            io.ResetColor();

            foreach (var item in response.Headers.OrderBy(header => header.Name))
            {
                io.ChangeColor(ConsoleColor.Blue);
                io.Output.Write(item.Name);
                io.ChangeColor(ConsoleColor.Green);
                io.Output.Write(":");
                io.ChangeColor(ConsoleColor.Magenta);
                io.Output.WriteLine(item.Value);
                io.ResetColor();
            }

            io.Output.WriteLine();
            io.Output.WriteLine(response.Content);   
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
