using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using WeatherApp.Service.Weather;
using WeatherApp.Services;
using WeatherApp.Services.Weather.Response;

namespace WeatherApp.UnitTest
{
	[TestClass]
	public class WeatherServiceTests
	{
		#region fields

		private WeatherService _weatherService;
		private Mock<IHttpRequestHandler> _httpRequestHandlerMock;

		#endregion

		#region Methods

		[TestInitialize]
		public void Initialize()
		{
			this._httpRequestHandlerMock = new Mock<IHttpRequestHandler>();
		}

		#region Get

		[TestMethod]
		public async Task GetWeather_DataExists()
		{
			string city = "Jakarta";
			var data = new WeatherResponse()
			{
				Name = city,
				Dt = 253402300790,
				Wind = new WindInfo()
				{
					Speed = 10
				},
				Visibility = 10,
				Weather = new List<WeatherDescription>()
				{
					new WeatherDescription()
					{
						Description = "weather description"
					}
				},
				Main = new MainInfo()
				{
					Temp = 10M,
					Humidity = 2,
					Pressure = 200,
				}
			};

			var expectedStatusCode = HttpStatusCode.OK;

			var httpContentWrapperMock = new Mock<IHttpContentWrapper>();
			httpContentWrapperMock.Setup(content => content.ReadAsStringAsync()).ReturnsAsync(JsonConvert.SerializeObject(data));

			var httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = expectedStatusCode,
				Content = new StringContent(await httpContentWrapperMock.Object.ReadAsStringAsync())
			};

			_httpRequestHandlerMock = new Mock<IHttpRequestHandler>();
			_httpRequestHandlerMock.Setup(handler => handler.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);
			this._weatherService = new WeatherService(_httpRequestHandlerMock.Object);

			var response = await this._weatherService.GetWeather(city).ConfigureAwait(true);

			Assert.IsNotNull(response);
			Assert.AreEqual(response.Location, data.Name);
		}


		[TestMethod]
		public async Task GetWeather_DataNotExists()
		{
			var data = new WeatherResponse();

			var expectedStatusCode = HttpStatusCode.BadGateway;

			var httpContentWrapperMock = new Mock<IHttpContentWrapper>();
			httpContentWrapperMock.Setup(content => content.ReadAsStringAsync()).ReturnsAsync(JsonConvert.SerializeObject(data));

			var httpResponseMessage = new HttpResponseMessage
			{
				StatusCode = expectedStatusCode,
				Content = new StringContent(await httpContentWrapperMock.Object.ReadAsStringAsync())
			};

			_httpRequestHandlerMock = new Mock<IHttpRequestHandler>();
			_httpRequestHandlerMock.Setup(handler => handler.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponseMessage);
			this._weatherService = new WeatherService(_httpRequestHandlerMock.Object);

			string city = "Jakarta";
			var response = await this._weatherService.GetWeather(city).ConfigureAwait(true);

			Assert.IsNull(response);
		}
		#endregion


		#endregion

	}
}
