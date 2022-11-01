using System;
using Newtonsoft.Json;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class UpdateDeviceTokenResponse : BaseResponse
    {
        [JsonProperty("saved")]
        public bool IsSaved { get; set; }
      
    }
}
