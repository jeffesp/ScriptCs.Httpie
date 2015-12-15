using System;
using System.Linq;
using RestSharp;

namespace ScriptCs.Httpie
{
    public static class ResponseExtensions
    {
        public static void WriteToHost(this IRestResponse response)
        {
            if (response == null)
            {
                Console.WriteLine("No Response");
                return;
            }

            // TODO: Add ProtocolVersion when it's available

            var currentColor = Console.ForegroundColor;
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

    }
}
