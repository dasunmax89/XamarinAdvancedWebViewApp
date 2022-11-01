﻿using System;
using Newtonsoft.Json;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Models
{
    public class BaseRequest
    {
        [JsonProperty("platform")]
        public string Message { get; set; }

        [JsonIgnore]
        public string EndPoint { get; set; }

        public BaseRequest()
        {
        }
    }
}
