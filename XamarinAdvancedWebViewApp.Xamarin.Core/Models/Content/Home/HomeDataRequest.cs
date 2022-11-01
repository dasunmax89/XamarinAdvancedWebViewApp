using System;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class HomeDataRequest : BaseRequest
    {
        public HomeDataRequest()
        {
            EndPoint = ApiConstants.GetHomeDataEndPoint;
        }
    }
}
