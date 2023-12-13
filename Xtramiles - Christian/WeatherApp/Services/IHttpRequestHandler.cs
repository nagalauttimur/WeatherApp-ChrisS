using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
	public interface IHttpRequestHandler
	{
		Task<HttpResponseMessage> GetAsync(string url);
	}
}
