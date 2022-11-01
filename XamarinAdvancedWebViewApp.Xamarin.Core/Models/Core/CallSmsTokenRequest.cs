using System;
using Newtonsoft.Json;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class CallSmsTokenRequest
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("deviceToken")]
        public string DeviceToken { get; set; }

        [JsonProperty("appVersion")]
        public string AppVersion { get; set; }

        [JsonProperty("devicePlatform")]
        public string DevicePlatform { get; set; }
    }
}
