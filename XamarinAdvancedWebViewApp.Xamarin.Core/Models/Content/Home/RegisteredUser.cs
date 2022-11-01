using System;
using Newtonsoft.Json;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class RegisteredUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
