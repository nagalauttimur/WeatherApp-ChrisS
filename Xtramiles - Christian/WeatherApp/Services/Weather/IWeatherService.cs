using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Service.Weather
{
    public interface IWeatherService
    {
        Task<WeatherVm> GetWeather(string city);
    }
}
