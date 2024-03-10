using Microsoft.AspNetCore.Mvc;
using SERVICES.Contexts;
using SERVICES.Models;
using System.Text.Json.Serialization;

namespace SERVICES.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly ApplicationDbContext _context; // Inyectamos el DbContext en el constructor

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Test", Name = "Test")]
        public IActionResult Test()
        {
            List<Producto> productos = this._context.Productos.Where(x => x.Nombre.StartsWith("Producto")).ToList();
            return Ok(new { message = "Funciono paaaa", data = productos });
        }
    }
}