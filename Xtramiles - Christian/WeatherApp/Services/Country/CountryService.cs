using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using WeatherApp.Services;
using WeatherApp.Services.Weather.Response;

namespace WeatherApp.Service.Country
{
	public class CountryService : ICountryService
	{
		private readonly IFileOperations _fileOperations;
		public CountryService(IFileOperations fileOperations)
		{
			_fileOperations = fileOperations;
		}

		public IEnumerable<string> GetCities(string country)
		{
			try
			{
				var jsonContent = _fileOperations.ReadAllText("~\\Data\\CountryData.txt");
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
				var jsonContent = _fileOperations.ReadAllText("~\\Data\\CountryData.txt");
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
