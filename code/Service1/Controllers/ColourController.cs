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
    public class ColourController : ControllerBase
    {
        private string getRGBHexValue()
        {
            Random random = new Random();
            return random.Next(255).ToString("X");
        }
        

        [HttpGet]
        public ActionResult<string> getColour()
        {

            string rgb = "#" + getRGBHexValue() + getRGBHexValue() + getRGBHexValue() ;
            return rgb;
        }
    }
}
