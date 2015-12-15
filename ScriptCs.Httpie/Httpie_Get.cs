using System;
using System.Net;

namespace ScriptCs.Httpie
{
    public partial class Httpie
    {
        public void Get()
        {
            Execute();
        }
        public void Get(string url)
        {
            Url(url).Execute();
        }

        public Httpie Url(string url)
        {
            uri = new Uri(urlParser.ParseUrl(url), UriKind.Absolute);
            return this;
        }
        public Httpie Port(ushort port)
        {
            var builder = new UriBuilder(uri);
            builder.Port = port;
            uri = builder.Uri;
            return this;
        }

        public Httpie Query(string query)
        {
            UriBuilder builder = new UriBuilder(uri);
            if (builder.Query.Length > 1)
                builder.Query = builder.Query.Substring(1) + "&" + query;
            else
                builder.Query = query;

            uri = builder.Uri;
            return this;
        }

        public Httpie Query(string name, string value)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Query($"{WebUtility.UrlEncode(name)}={WebUtility.UrlEncode(value)}");
        }

        public Httpie Header(string name, string value)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            restRequest.AddHeader(name, value);
            return this;
        }

    }
}
