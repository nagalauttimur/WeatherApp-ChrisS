using System.Web.Mvc;

namespace WeatherApp.Models
{
    public class IndexVm
    {
        public string Country { get; set; } 
        public SelectList Countries { get; set; }
        public string City { get; set; }
        public SelectList Cities { get; set; }
    }
}
