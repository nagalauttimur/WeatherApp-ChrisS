using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp.Service.Country
{
    public interface ICountryService
    {
        Task<IEnumerable<string>> GetCountries();
		IEnumerable<string> GetCities(string country);
	}
}
