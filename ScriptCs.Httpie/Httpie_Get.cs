using System;
using System.Net;
using System.Net.Mime;

namespace ScriptCs.Httpie
{
    public partial class Httpie
    {
        /// <summary>
        /// Performs a GET request on the configured Httpie object.
        /// </summary>
        public void Get()
        {
            Execute();
        }

        /// <summary>
        /// Performs a GET request on the url.
        /// </summary>
        /// <param name="url">The url. Can either start with scheme, or assumed http if no scheme specified.</param>
        public void Get(string url)
        {
            Url(url).Execute();
        }

        /// <summary>
        /// Set the url to request.
        /// </summary>
        /// <param name="url">The url. Can either start with scheme, or assumed http if no scheme specified.</param>
        /// <returns>The Httpie instance.</returns>
        public Httpie Url(string url)
        {
            uri = new Uri(LiberalUrlParser.ParseUrl(url), UriKind.Absolute);
            return this;
        }

        /// <summary>
        /// Set the port for the host
        /// </summary>
        /// <param name="port">The port number. 0-65535</param>
        /// <returns>The Httpie instance.</returns>
        public Httpie Port(ushort port)
        {
            var builder = new UriBuilder(uri);
            builder.Port = port;
            uri = builder.Uri;
            return this;
        }

        /// <summary>
        /// Adds additional item(s) to the query string.
        /// </summary>
        /// <param name="query">The query "name=value" or "name=value&amp;name2=value2" format.</param>
        /// <returns>The Httpie instance</returns>
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

        /// <summary>
        /// Adds an element to the query string.
        /// </summary>
        /// <param name="name">The name of the </param>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a header to the request.
        /// </summary>
        /// <param name="name">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The Httpie instance.</returns>
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

        /// <summary>
        /// Set the accept header value.
        /// </summary>
        /// <param name="contentType">The value as a <see cref="System.Net.Mime.ContentType"/>.</param>
        /// <returns>The Httpie instance.</returns>
        public Httpie Accept(ContentType contentType)
        {
            return Header("accept", contentType.ToString());
        }

        /// <summary>
        /// Set the accept header value.
        /// </summary>
        /// <param name="contentType">The value as a String.</param>
        /// <returns>The Httpie instance.</returns>
        public Httpie Accept(string contentType)
        {
            return Accept(new ContentType(contentType));
        }

    }
}
