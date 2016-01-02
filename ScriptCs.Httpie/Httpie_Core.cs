using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using RestSharp;
using ScriptCs.Contracts;
using System.IO;

namespace ScriptCs.Httpie
{
    public partial class Httpie : IScriptPackContext
    {
        private Uri uri;

        private readonly Lazy<string> assemblyVersion;
        private readonly Lazy<string> typeName;

        private readonly IRestClient restClient;
        private readonly IRestRequest restRequest;

        public Httpie() : this(new RestClient(), new RestRequest())
        {
            
        }

        public Httpie(IRestClient restClient, IRestRequest restRequest)
        {
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
            return this;
        }

        /// <summary>
        /// Changes the output destination to the specified file.
        /// </summary>
        /// <param name="fileName">The file to use as output. The file extension will be used to figure out the serialization format.</param>
        public Httpie SetOutput(string fileName)
        {
            return this;
        }

        private void Execute()
        {
            ExecuteInternal();
            var response = restClient.Execute(restRequest);
            response.WriteToHost();
        }

        private async Task ExecuteAsync()
        {
            ExecuteInternal();
            var response = await restClient.ExecuteTaskAsync(restRequest);
            response.WriteToHost();
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


    }
}