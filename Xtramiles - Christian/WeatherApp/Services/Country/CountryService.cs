using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Services.Weather.Response;

namespace WeatherApp.Service.Country
{
	public class CountryService : ICountryService
	{
		//need to update this file later after found api that contain country and city
		private const string jsonFilePath = "D:\\Users\\ayu.p\\source\\repos\\Xtramiles - Christian\\WeatherApp\\Data\\CountryData.txt";

		public CountryService()
		{
		}

		public IEnumerable<string> GetCities(string country)
		{
			try
			{
				var jsonContent = System.IO.File.ReadAllText(jsonFilePath);
				var countryResponse = JsonConvert.DeserializeObject<List<CountryResponse>>(jsonContent).Where(item => item.CountryCode == country).FirstOrDefault();
				var cities = new List<string>();
				if (countryResponse != null)
				{
					cities = countryResponse.Cities.ToList();
				}

				return cities;
			}
			catch (Exception ex)
			{
				// Handle exceptions
				return null;
			}
		}

		public async Task<IEnumerable<string>> GetCountries()
		{
			try
			{
				var jsonContent = System.IO.File.ReadAllText(jsonFilePath);
				var countries = JsonConvert.DeserializeObject<List<CountryResponse>>(jsonContent);
				return countries.Select(item => item.CountryCode).OrderBy(item => item);
			}
			catch (Exception ex)
			{
				// Handle exceptions
				return null;
			}
		}
	}
}
