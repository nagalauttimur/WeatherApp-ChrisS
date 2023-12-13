using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Service.Country;

namespace WeatherApp.UnitTest
{
	[TestClass]
	public class CountryServiceTests
	{
		#region fields

		private CountryService _countryService;

		#endregion

		#region Methods

		[TestInitialize]
		public void Initialize()
		{
			this._countryService = new CountryService();
		}

		#region Get

		[TestMethod]
		public async Task GetCountries_DataExists()
		{
			var response = await this._countryService.GetCountries().ConfigureAwait(true);

			Assert.IsTrue(response.Count() > 0);
		}

		[TestMethod]
		public void GetCities_DataExists()
		{
			string country = "Brazil";
			var response = this._countryService.GetCities(country);

			Assert.IsTrue(response.Count() > 0);
		}

		[TestMethod]
		public void GetCities_DataNotExists()
		{
			string country = "Indonesia";
			var response = this._countryService.GetCities(country);

			Assert.IsTrue(response.Count() == 0);
		}

		#endregion

		#endregion

	}
}
