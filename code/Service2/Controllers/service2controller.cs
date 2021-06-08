using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class service2controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<service2controller> _logger;

        public service2controller(ILogger<service2controller> logger)
        {
            _logger = logger;
        }
    }
}
