using System;
using Newtonsoft.Json;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class UpdateDeviceTokenRequest
    {
        [JsonProperty("deviceToken")]
        public string DeviceToken { get; set; }

        [JsonProperty("devicePlatform")]
        public string DevicePlatform { get; set; }
    }
}
