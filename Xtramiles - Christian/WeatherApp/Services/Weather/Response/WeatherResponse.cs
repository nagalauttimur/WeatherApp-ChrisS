using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherApp.Services.Weather.Response
{
    public class WeatherResponse
    {
        public string Name { get; set; }

        public long Dt { get; set; }

        public MainInfo Main { get; set; }

        public WindInfo Wind { get; set; }

        public int Visibility { get; set; }

        public List<WeatherDescription> Weather { get; set; }
    }
    public class MainInfo
    {
        [JsonPropertyName("temp")]
        public decimal Temp { get; set; }

        public int Humidity { get; set; }

        public int Pressure { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
    }

    public class WeatherDescription
    {
        public string Description { get; set; }
    }
}
