using System;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using RestSharp;
using ScriptCs.Contracts;
using ScriptCs.Httpie.Streams;

namespace ScriptCs.Httpie
{
    // This is the core or this project.
    public partial class Httpie : IScriptPackContext, IDisposable
    {
        private Uri uri;

        private readonly Lazy<string> assemblyVersion;
        private readonly Lazy<string> typeName;

        private readonly IRestClient restClient;
        private readonly IRestRequest restRequest;
        private readonly InputOutput io;

        public Httpie() : this(new RestClient(), new RestRequest(), new InputOutput())
        {
            
        }

        public Httpie(IRestClient restClient, IRestRequest restRequest, InputOutput io)
        {
            this.io = io;
            this.restClient = restClient;
            this.restRequest = restRequest;

            typeName = new Lazy<string>(() => this.GetType().FullName);
            assemblyVersion = new Lazy<string>(() =>
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileVersion;
            });
        }

        /// <summary>
        /// Changes the input source to be the specified file. 
        /// </summary>
        /// <param name="fileName">The file to use as input. The file extension will be used to figure out the serialization format.</param>
        public Httpie SetInput(string fileName)
        {
            io.SetInput(new FileStreamReader(fileName));
            return this;
        }

        /// <summary>
        /// Changes the output destination to the specified file.
        /// </summary>
        /// <param name="fileName">The file to use as output. The file extension will be used to figure out the serialization format.</param>
        public Httpie SetOutput(string fileName)
        {
            manageOutputStream = true;
            io.SetOutput(new FileStreamWriter(fileName));
            return this;
        }

        /// <summary>
        /// Changes the output destination to the specified file. Assumes that you are going to manage the lifetime of the stream.
        /// </summary>
        /// <param name="fileName">The file to use as output. The file extension will be used to figure out the serialization format.</param>
        public Httpie SetOutput(Stream output)
        {
            manageOutputStream = false;
            io.SetOutput(new StreamStreamWriter(output));
            return this;
        }

        private void Execute()
        {
            ExecuteInternal();
            var response = restClient.Execute(restRequest);
            response.WriteToHost(io);
        }

        private async Task ExecuteAsync()
        {
            ExecuteInternal();
            var response = await restClient.ExecuteTaskAsync(restRequest);
            response.WriteToHost(io);
        }

        private void ExecuteInternal()
        {
            restClient.BaseUrl = new Uri(uri.GetLeftPart(UriPartial.Authority));
            restClient.UserAgent = $"{typeName.Value}/{assemblyVersion.Value}";

            restRequest.Resource = uri.AbsolutePath.Substring(1);
            if (!String.IsNullOrEmpty(uri.Query))
            {
                var queryParameters = QueryStringParser.ParseQueryString(uri.Query);
                foreach (string key in queryParameters.Keys)
                {
                    restRequest.AddQueryParameter(key, queryParameters[key]);
                }
            }
        }

        private bool disposedValue = false; 
        private bool manageOutputStream = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                io.Flush();
                if (disposing && manageOutputStream)
                {
                    io.Output.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        } 

    }
}