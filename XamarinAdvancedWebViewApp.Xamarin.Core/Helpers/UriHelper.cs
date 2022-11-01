﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Helpers
{
    public static class UriHelper
    {
        public static string CombineUri(params string[] uriParts)
        {
            string uri = string.Empty;
            if (uriParts != null && uriParts.Count() > 0)
            {
                char[] trims = new char[] { '\\', '/' };
                uri = (uriParts[0] ?? string.Empty).TrimEnd(trims);
                for (int i = 1; i < uriParts.Count(); i++)
                {
                    uri = string.Format("{0}/{1}", uri.TrimEnd(trims), (uriParts[i] ?? string.Empty).TrimStart(trims));
                }
            }
            return uri;
        }

        public static string CombineUriParamsFromDictionary(string uri, IDictionary<string, string> dict)
        {
            string paramString = string.Join("&", dict.Select((x) => x.Key + "=" + x.Value));

            string combinedUri = string.Format("{0}?{1}", uri, paramString);

            return combinedUri;
        }
    }
}
