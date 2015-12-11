using System;
using System.Collections.Specialized;

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
                int keyMatchStart = i;

                while (i < query.Length)
                {
                    if (query[i] == '=')
                    {
                        key = query.Substring(keyMatchStart, i - keyMatchStart);
                        keyMatchStart = i + 1;
                    }
                    else if (query[i] == '&')
                    {
                        value = query.Substring(keyMatchStart, i - keyMatchStart);
                        keyMatchStart = i + 1;
                        break;
                    }
                    i++;
                }

                if (i == query.Length)
                {
                    value = query.Substring(keyMatchStart, i - keyMatchStart);
                }

                result.Add(key, value);
                i++;
            }

            return result;
        }
    }
}