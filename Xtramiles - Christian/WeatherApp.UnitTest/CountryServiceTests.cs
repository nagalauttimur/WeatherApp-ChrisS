using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using WeatherApp.Service.Country;
using WeatherApp.Services;
using WeatherApp.Services.Weather.Response;

namespace WeatherApp.UnitTest
{
	[TestClass]
	public class CountryServiceTests
	{
		#region fields

		private CountryService _countryService;
		private Mock<IFileOperations> _fileOperationsMock;

		#endregion

		#region Methods

		[TestInitialize]
		public void Initialize()
		{
			this._fileOperationsMock = new Mock<IFileOperations>();
			this._countryService = new CountryService(_fileOperationsMock.Object);
		}

		#region Get

		[TestMethod]
		public async Task GetCountries_DataExists()
		{
			string city = "Jakarta";
			var data = new List<CountryResponse>()
			{
				new CountryResponse()
				{
					CountryCode = "Indonesia",
					Cities = new List<string>()
					{
						city,"Bandung"
					}
				}
			};

			_fileOperationsMock.Setup(handler => handler.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(data));
			var response = await this._countryService.GetCountries().ConfigureAwait(true);

			Assert.AreEqual(response.Count(), 1);
		}

		[TestMethod]
		public void GetCities_DataExists()
		{
			string country = "Indonesia";
			var data = new List<CountryResponse>()
			{
				new CountryResponse()
				{
					CountryCode = "Indonesia",
					Cities = new List<string>()
					{
						"Jakarta","Bandung"
					}
				}
			};

			_fileOperationsMock.Setup(handler => handler.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(data));
			var response = this._countryService.GetCities(country);

			Assert.IsTrue(response.Count() > 0);
		}

		[TestMethod]
		public void GetCities_DataNotExists()
		{
			string country = "Indonesia";
			var data = new List<CountryResponse>();

			_fileOperationsMock.Setup(handler => handler.ReadAllText(It.IsAny<string>())).Returns(JsonConvert.SerializeObject(data));
			var response = this._countryService.GetCities(country);

			Assert.IsTrue(response.Count() == 0);
		}

		#endregion

		#endregion

	}
}
