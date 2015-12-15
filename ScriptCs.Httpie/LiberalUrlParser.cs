﻿using System;

namespace ScriptCs.Httpie
{
    public class LiberalUrlParser
    {
        public string ParseUrl(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Url must be specified.", nameof(url));
            }

            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            return $"http://{url}";
        }
    }
}