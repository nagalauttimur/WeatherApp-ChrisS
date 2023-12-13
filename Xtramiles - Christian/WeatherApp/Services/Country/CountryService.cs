using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using WeatherApp.Services.Weather.Response;

namespace WeatherApp.Service.Country
{
	public class CountryService : ICountryService
	{
		public CountryService()
		{
		}

		public IEnumerable<string> GetCities(string country)
		{
			var jsonFilePath = HttpContext.Current.Server.MapPath("~\\Data\\CountryData.txt");
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
			var jsonFilePath = HttpContext.Current.Server.MapPath("~\\Data\\CountryData.txt");
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
