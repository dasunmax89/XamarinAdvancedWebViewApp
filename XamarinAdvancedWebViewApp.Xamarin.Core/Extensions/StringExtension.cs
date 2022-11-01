using System;
using System.Text.RegularExpressions;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Extensions
{
    public static class StringExtension
    {
        public static string RemoveNewLines(this string stringVal)
        {
            string formattedText = string.Empty;

            formattedText = stringVal?.Replace(System.Environment.NewLine, string.Empty);

            return formattedText;
        }

        public static long ToNumber(this string stringVal)
        {
            bool canParse = long.TryParse(stringVal, out long numberValue);

            return numberValue;
        }

        public static bool Search(this string stringVal, string text)
        {
            if (string.IsNullOrEmpty(stringVal) || string.IsNullOrEmpty(text))
            {
                return false;
            }

            bool contains = stringVal.ToLower().Contains(text.ToLower());

            return contains;
        }

        public static bool Match(this string stringVal, string text)
        {
            if (string.IsNullOrEmpty(stringVal) || string.IsNullOrEmpty(text))
            {
                return false;
            }

            bool equals = stringVal.Equals(text, StringComparison.InvariantCultureIgnoreCase);

            return equals;
        }

        public static bool IsURI(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Uri uriResult;
            bool isURI = Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isURI;
        }

        public static string RemoveInvalidText(this string stringVal)
        {
            //emojies
            string text = Regex.Replace(stringVal, @"[^\u0000-\u007F]+", string.Empty);

            //html
            text = Regex.Replace(text, "<.*?>", string.Empty);

            return text.Trim();
        }
    }
}
