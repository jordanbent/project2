using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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

        /*
            colourLogic - takes in colour label as a string and returns a corresponding string
            with a animal name that is the colour given. 
        */
        private string colourLogic(string colourName, int plural)
        {
            if (plural == 1)
            {
                switch (colourName)
                {
                    case "White":
                        return "Dove";
                    case "Grey":
                        return "Mouse";
                    case "Black":
                        return "Crow";
                    case "Blue":
                        return "Whale";
                    case "Red":
                        return "Fox";
                    case "Green":
                        return "Lizard";
                    case "Yellow":
                        return "Snake";
                    default:
                        return "";
                }
            }
            else
            {
                switch (colourName)
                {
                    case "White":
                        return "Doves";
                    case "Grey":
                        return "Mice";
                    case "Black":
                        return "Crows";
                    case "Blue":
                        return "Whales";
                    case "Red":
                        return "Foxes";
                    case "Green":
                        return "Lizards";
                    case "Yellow":
                        return "Snakes";
                    default:
                        return "";
                }
            }
        }

        /*
            fruitLogic - takes in fruit name as a string and returns a corresponding string
            with a prediction of the users future, based on the fruit given to them. 
        */
        private int fruitLogic()
        {
            Random random = new Random();
            return random.Next(minValue: 1, maxValue: 100);
        }

        /*
            Get - HTTPGet Request; using configuration settings of the mergeController, the 
            colourServiceURL and fruitServiceURL are obtained (stored in appsettings.json locally, 
            and stored in application settings on Azure Web Apps), each API, colourService and fruitService, 
            is called and the data is returned.
            Using the two above functions, the logic of the mergeController is applied and concatonated. This
            string is then sent in the HTTP request.  
        */

        private string makeSentence(string colour, string colourname, string fruit, int number)
        {
            if (number == 1)
                return "There is " + number + " " + colourname.ToLower() + " " + colourLogic(colourname, 1).ToLower() + " eating " + fruit.ToLower() + ".";
            else
                return "There are " + number + " " + colourname.ToLower() + " " + colourLogic(colourname, 0).ToLower() + " eating " + fruit.ToLower() + ".";
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colourService = $"{Configuration["colourServiceURL"]}/colour";
            var fruitService = $"{Configuration["fruitServiceURL"]}/fruit";

            var colour = "";
            var colourName = "";
            var fruit = "";
            var number = 0;

            var serviceOneResponseCall = await new HttpClient().GetStringAsync(colourService);
            var serviceTwoResponseCall = await new HttpClient().GetStringAsync(fruitService);

            var colourResponse = serviceOneResponseCall.Split("/");
            var fruitResponse = serviceTwoResponseCall;
            colour = colourResponse[0];
            colourName = colourResponse[1];
            fruit = fruitResponse;
            number = fruitLogic();

            var mergedResponse = "" + colour + "\n" + makeSentence(colour, colourName, fruit, number);
            return Ok(mergedResponse);
        }
        
    }
}
