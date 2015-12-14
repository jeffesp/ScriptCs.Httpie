using System;
using System.Collections.Specialized;
using System.Net;

namespace ScriptCs.Httpie
{
    public static class QueryStringParser
    {
        public static NameValueCollection ParseQueryString(string query)
        {
            var result = new NameValueCollection();

            if (String.IsNullOrWhiteSpace(query))
            {
                return result;
            }

            int i = 0;

            if (query[0] == '?')
                i++;

            while (i < query.Length)
            {
                string key = null, value = null;
                int matchStart = i;

                // inner loop here is what really traverses the string, outer loop is just there
                // mainly to add the results
                while (i < query.Length)
                {
                    if (query[i] == '=')
                    {
                        key = query.Substring(matchStart, i - matchStart);
                        matchStart = i + 1;
                    }
                    else if (query[i] == '&')
                    {
                        value = query.Substring(matchStart, i - matchStart);
                        matchStart = i + 1;
                        break;
                    }
                    i++;
                }

                // this is the case of the last entry - we didn't have a '&' to match in the above
                // loop and set the 'value' variable
                if (i == query.Length)
                {
                    value = query.Substring(matchStart, i - matchStart);
                }

                result.Add(key, WebUtility.UrlDecode(value));
                i++;
            }

            return result;
        }
    }
}