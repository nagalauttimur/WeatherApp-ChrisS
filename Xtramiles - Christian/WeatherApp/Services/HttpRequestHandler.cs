using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
	public class HttpRequestHandler : IHttpRequestHandler
	{
		private readonly HttpClient _httpClient;

		public HttpRequestHandler(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Task<HttpResponseMessage> GetAsync(string url)
		{
			return _httpClient.GetAsync(url);
		}
	}
}