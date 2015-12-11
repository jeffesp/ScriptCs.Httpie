using System;
using RestSharp;
using ScriptCs.Contracts;

namespace ScriptCs.Httpie
{
    public class Httpie : IScriptPackContext
    {
        private Uri uri;
        private short port;
        private LiberalUrlParser urlParser = new LiberalUrlParser();
        private IRestClient restClient;
        private IRestRequest restRequest;

        public Httpie() : this(new RestClient(), new RestRequest())
        {
            
        }

        public Httpie(IRestClient restClient, IRestRequest restRequest)
        {
            this.restClient = restClient;
            this.restRequest = restRequest;
        }

        public Httpie Port(short port)
        {
            this.port = port;
            return this;
        }

        public Httpie Url(string url)
        {
            uri = new Uri(urlParser.ParseUrl(url), UriKind.Absolute);
            return this;
        }

        public Httpie Get()
        {
            return this;
        }

        public Httpie Get(string url)
        {
            uri = new Uri(urlParser.ParseUrl(url), UriKind.Absolute);
            return this;
        }

        //public Httpie AddQueryParam(string key, string value)
        //{
        //    return this;
        //}

        public void Execute()
        {
            restClient.BaseUrl = new Uri(uri.GetLeftPart(UriPartial.Authority));
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