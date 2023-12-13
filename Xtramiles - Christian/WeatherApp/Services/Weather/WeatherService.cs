using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Services.Weather.Response;

namespace WeatherApp.Service.Weather
{
	public class WeatherService : IWeatherService
	{
		private readonly IHttpRequestHandler _httpRequestHandler;
		private const string ApiKey = "f999bc338dbbe79ec48cc00ce4ceacd8";
		private const string WeatherApiUrl = "http://api.openweathermap.org/data/2.5/weather";

		public WeatherService(IHttpRequestHandler httpRequestHandler)
		{
			this._httpRequestHandler = httpRequestHandler;
		}

		public async Task<WeatherVm> GetWeather(string city)
		{
			try
			{
				var apiUrl = $"{WeatherApiUrl}?q={city}&appid={ApiKey}";

				var response = await this._httpRequestHandler.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					var content = await new HttpContentWrapper(response.Content).ReadAsStringAsync();
					var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(content);

					var weather = new WeatherVm
					{
						Location = weatherResponse.Name,
						Time = DateTimeOffset.FromUnixTimeSeconds(weatherResponse.Dt).DateTime.ToString("dd-MMM-yyyy HH:mm"),
						Wind = $"{weatherResponse.Wind.Speed} m/s",
						Visibility = $"{weatherResponse.Visibility} meters",
						SkyConditions = weatherResponse.Weather[0].Description,
						TemperatureCelsius = weatherResponse.Main.Celcius,
						TemperatureFahrenheit = weatherResponse.Main.Fahrenheit,
						DewPoint = $"{weatherResponse.Main.Temp} K",
						RelativeHumidity = $"{weatherResponse.Main.Humidity}%",
						Pressure = $"{weatherResponse.Main.Pressure} hPa"
					};

					return weather;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
