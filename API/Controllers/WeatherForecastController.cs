using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" 
        };

        private readonly ILogger<WeatherForecastController>_logger;

        static readonly string[] scopeRequiredByApi = new string[] {"access_as_user"};

        public WeatherForecastController(ILogger<WeatherForecastController> logger){
            _logger = logger;
        }
       
    }
}