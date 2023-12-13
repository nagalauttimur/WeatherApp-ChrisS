using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
	public class HttpContentWrapper : IHttpContentWrapper
	{
		private readonly HttpContent _httpContent;

		public HttpContentWrapper(HttpContent httpContent)
		{
			_httpContent = httpContent;
		}

		public Task<string> ReadAsStringAsync()
		{
			return _httpContent.ReadAsStringAsync();
		}
	}
}