using System;
using System.Diagnostics;
using System.Reflection;
using RestSharp;
using ScriptCs.Contracts;

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

        private void Execute()
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

            var response = restClient.Execute(restRequest);
            response.WriteToHost();
        }

    }
}