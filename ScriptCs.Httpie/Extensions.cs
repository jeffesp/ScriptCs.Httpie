using System;
using System.IO;
using System.Linq;
using RestSharp;

namespace ScriptCs.Httpie
{
    public static class Extensions
    {
        public static void WriteToHost(this IRestResponse response)
        {
            if (response == null)
            {
                Console.WriteLine("No Response");
                return;
            }

            // TODO: Add ProtocolVersion when it's available

            Console.ForegroundColor = (int)response.StatusCode > 299 ? ConsoleColor.Red : ConsoleColor.DarkGreen;
            Console.WriteLine("{0} {1}", (int)response.StatusCode, response.StatusDescription);
            Console.ResetColor();

            foreach (var item in response.Headers.OrderBy(header => header.Name))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(item.Name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(":");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(item.Value);
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine(response.Content);   
        }

        public static void WriteTo(this IRestResponse response, TextWriter output)
        {
            Console.SetOut(output); 
            WriteToHost(response);
        }

        public static void WriteToHost(this IRestRequest request)
        {
            Console.WriteLine(request.Method);
            Console.WriteLine(request.Resource);
            foreach (var parameter in request.Parameters)
            {
                Console.WriteLine("Parameter type: {0}", parameter.Type);
                Console.WriteLine(parameter);
            }
        }

        public static void WriteTo(this IRestRequest request, TextWriter output)
        {
            Console.SetOut(output); 
            WriteToHost(request);
        }
    }
}
