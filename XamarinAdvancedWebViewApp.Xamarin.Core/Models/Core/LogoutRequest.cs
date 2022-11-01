using System;
namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class LogoutRequest
    {
        public LogoutRequest()
        {
        }

        public string AuthAccessToken { get; internal set; }
    }
}
