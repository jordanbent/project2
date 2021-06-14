using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MergeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MergeController : ControllerBase
    {
        private IConfiguration Configuration;
        public MergeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colourService = $"{Configuration["colourService"]}/colour";
            var colour = "";
            var colourName = "";
            var serviceOneResponseCall = await new HttpClient().GetStringAsync(colourService);

            var colourResponse = serviceOneResponseCall.Split("/");
            colour = colourResponse[0];
            colourName = colourResponse[1];

            var fruitService = $"{Configuration["fruitService"]}/fruit";
            var serviceTwoResponseCall = await new HttpClient().GetStringAsync(fruitService);

            var mergedResponse = $"{colour}\n{colourName}\n{serviceTwoResponseCall}";
            return Ok(mergedResponse);
        }
    }
}
