using System.Threading.Tasks;

namespace WeatherApp.Services
{
	public interface IHttpContentWrapper
	{
		Task<string> ReadAsStringAsync();
	}
}
