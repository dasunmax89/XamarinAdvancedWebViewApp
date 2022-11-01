using Newtonsoft.Json;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class ShopItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string ImageUrl { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}