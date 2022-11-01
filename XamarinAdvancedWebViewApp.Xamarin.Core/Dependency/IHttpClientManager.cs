using System;
using System.Net.Http;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Dependency
{
    public interface IHttpClientManager
    {
        HttpClient GetHttpClient();
        HttpClient GetHttpClientForAuthentication();
    }
}
