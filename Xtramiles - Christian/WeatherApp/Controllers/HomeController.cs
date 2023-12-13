using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WeatherApp.Models;
using WeatherApp.Service.Country;
using WeatherApp.Service.Weather;

namespace WeatherApp.Controllers
{
	public class HomeController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly IWeatherService _weatherService;

        public HomeController(ICountryService countryService, IWeatherService weatherService)
        {
            this._countryService = countryService;
            this._weatherService = weatherService;
        }

        public async Task<ActionResult> Index()
        {
            var model = new IndexVm();
            var countries = await this._countryService.GetCountries().ConfigureAwait(true);
            model.Countries = PopulateCountryList(countries); 
            model.Cities = PopulateCityList(countries);

			return View(model);
        }

        [HttpPost]
        public JsonResult GetCities(string country)
        {
            var cities = this._countryService.GetCities(country);
            return Json(new { IsSuccess = true, Cities = cities });
        }


		[HttpPost]
		public async Task<ActionResult> GetWeather(string city)
        {
			if (string.IsNullOrEmpty(city))
			{
				return Json(new { IsSuccess = true, Weather = new WeatherVm() });
			}
            var weather = await this._weatherService.GetWeather(city).ConfigureAwait(true); ;
			return Json(new { IsSuccess = true, Weather = weather });
		}

		private SelectList PopulateCountryList(IEnumerable<string> countries)
		{
			var selectListItems = new Dictionary<string, string>();

			foreach (var country in countries)
			{
				selectListItems.Add(country, country);
			}

			return new SelectList(selectListItems, "Key", "Value");
		}

		private SelectList PopulateCityList(IEnumerable<string> cities)
		{
			var selectListItems = new Dictionary<string, string>();

			foreach (var city in cities)
			{
				selectListItems.Add(city, city);
			}

			return new SelectList(selectListItems, "Key", "Value");
		}
	}
}