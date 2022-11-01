using System;
using System.Net.Http;
using XamarinAdvancedWebViewApp.Xamarin.Core.Dependency;
using XamarinAdvancedWebViewApp.Xamarin.iOS.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpClientManager))]
namespace XamarinAdvancedWebViewApp.Xamarin.iOS.Dependency
{
    public class HttpClientManager : IHttpClientManager
    {
        public HttpClientManager()
        {
        }

        public HttpClient GetHttpClientForAuthentication()
        {
            var client = new HttpClient(new NSUrlSessionHandler()); ;

            return client;
        }

        public HttpClient GetHttpClient()
        {
            var client = new HttpClient(new NSUrlSessionHandler());

            return client;
        }
    }
}
