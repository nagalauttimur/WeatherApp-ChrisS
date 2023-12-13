using System.IO;
using System.Web;

namespace WeatherApp.Services
{
	public class FileOperations : IFileOperations
	{
		public string ReadAllText(string path)
		{
			var jsonPath = HttpContext.Current.Server.MapPath(path);
			return File.ReadAllText(jsonPath);
		}
	}
}