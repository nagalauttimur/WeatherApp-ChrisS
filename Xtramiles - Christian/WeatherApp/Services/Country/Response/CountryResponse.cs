using System.Collections.Generic;
using Newtonsoft.Json;
using WeatherApp.Models;

namespace WeatherApp.Services.Weather.Response
{
    public class CountryResponse
    {
        [JsonProperty("country")]
        public string CountryCode { get; set; }

        [JsonProperty("city")]
        public IEnumerable<string> Cities { get; set; }
    }
}
