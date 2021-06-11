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

        public string getColourString(string colourHex)
        {
            Console.WriteLine(colourHex.Length);
            // string = '#RRGGBB'
            double red = Int32.Parse(colourHex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber) + 1;
            Console.WriteLine("red= " + red);
            double green = Int32.Parse(colourHex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber) + 1;
            Console.WriteLine("g= " + green);
            double blue = Int32.Parse(colourHex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber) + 1;
            Console.WriteLine("blue= " + blue);

            // Difference Tolerance = 5%
            double RG = ((System.Math.Abs(red - green)) / 255) * 100;
            Console.WriteLine("RG= " + RG);
            double GB = ((System.Math.Abs(green - blue)) / 255) * 100;
            Console.WriteLine("GB= " + GB);
            double BR = ((System.Math.Abs(blue - red)) / 255) * 100;
            Console.WriteLine("BR= " + BR);

            //If red/green/blue are similar numbers
            if (RG < 5 && GB < 5 && BR < 5)
            {
                //White #FFFFFF - #DDDDDD 170-255
                if (red >= 170 && green >= 170 && blue >= 170)
                {
                    Console.WriteLine("White");
                    return "White";
                }
                //Grey #CCCCCC - #666666 85 -170
                if (red >= 85 && red < 170 && green >= 85 && green < 170 && blue >= 85 && blue < 170)
                {
                    Console.WriteLine("grey");
                    return "Grey";
                }
                //Black #555555 - #000000 0-85
                if (red < 85 && green < 85 && blue < 85)
                {
                    Console.WriteLine("Black");
                    return "Black";
                }
            }

            //Blue
            if (RG > 35 && blue > 150)
            {
                Console.WriteLine("Blue");
                return "Blue";
            }

            //Little to no blue
            if (blue < 100)
            {
                if (RG > 50)
                {
                    //Red
                    if (red > green)
                    {
                        Console.WriteLine("Red");
                        return "Red";
                    }
                    //Green
                    if (green > red)
                    {
                        Console.WriteLine("Green");
                        return "Green";
                    }
                }
                else if (RG < 15)
                {
                    //Yellow red=green !blue
                    Console.WriteLine("Yellow");
                    return "Yellow";
                }
            }

            //Brown red=(green/2)=(blue/4)
            //
            return "";
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colourService = $"{Configuration["colourService"]}/colour";
            var colourName = "";
            var serviceOneResponseCall = await new HttpClient().GetStringAsync(colourService);

            while (colourName.Equals(""))
            {
                serviceOneResponseCall = await new HttpClient().GetStringAsync(colourService);
                colourName = getColourString(serviceOneResponseCall);
            }

            var fruitService = $"{Configuration["fruitService"]}/fruit";
            var serviceTwoResponseCall = await new HttpClient().GetStringAsync(fruitService);

            var mergedResponse = $"{serviceOneResponseCall+"/"+colourName}\n{serviceTwoResponseCall}";
            return Ok(mergedResponse);
        }
    }
}
