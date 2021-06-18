using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColourController : ControllerBase
    { 
        private string getColourString(string colourHex)
        {
            Console.WriteLine(colourHex.Length);
            Console.WriteLine(colourHex);
            // string = '#RRGGBB'
            double red = Int32.Parse(colourHex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("red= " + red);
            double green = Int32.Parse(colourHex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("g= " + green);
            double blue = Int32.Parse(colourHex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
            Console.WriteLine("blue= " + blue);

            // Difference Percentage
            double RG = ((System.Math.Abs(red - green)) / 255) * 100;
            Console.WriteLine("RG= " + RG);
            double GB = ((System.Math.Abs(green - blue)) / 255) * 100;
            Console.WriteLine("GB= " + GB);
            double BR = ((System.Math.Abs(blue - red)) / 255) * 100;
            Console.WriteLine("BR= " + BR);

            //If red/green/blue are similar numbers
            if (RG < 10 && GB < 10 && BR < 10)
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
            else if (RG > 20 && red < 100 && green < 100 && blue > 100)
            {
                Console.WriteLine("Blue");
                return "Blue";
            }
            //Little to no blue
            else if (blue < 150)
            {
                if (RG > 70)
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
                else if (RG < 5 && blue < 50)
                {
                    //Yellow red=green !blue
                    Console.WriteLine("Yellow");
                    return "Yellow";
                }
            }
            return "Pink";
        }

        private string getRGBHexValue()
        {
            Random random = new Random();
            var hex = random.Next(255).ToString("X");
            if (hex.Length < 2)
                hex = "0" + hex;

            return hex;
        }

        [HttpGet]
        public ActionResult<string> getColour()
        {
            string rgbName = "Pink";
            string rgb = "";
            while (rgbName.Equals("Pink"))
            {
                rgb = "#" + getRGBHexValue() + getRGBHexValue() + getRGBHexValue();
                rgbName = getColourString(rgb);
            }

            return rgb+"/"+rgbName;
        }
    }
}
