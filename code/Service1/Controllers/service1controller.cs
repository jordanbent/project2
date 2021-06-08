using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class service1controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<service1controller> _logger;

        public service1controller(ILogger<service1controller> logger)
        {
            _logger = logger;
        }

    }
}
