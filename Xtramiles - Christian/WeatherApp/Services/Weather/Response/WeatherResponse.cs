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

		public decimal Celcius
		{
			get
			{;
				return decimal.Round(Temp - 273.15M, 2);
			}
		}

		public decimal Fahrenheit
		{
			get
			{
				return decimal.Round(Celcius * 9 / 5 + 32, 2);
			}
		}
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
