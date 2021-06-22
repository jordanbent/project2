using Microsoft.AspNetCore.Mvc;
using System;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColourController : ControllerBase
    {
        /* getColourString - takes a hexidecimal string that represents a colour
           that is labeled with the appropriate colour name. 
           If no colour name suits, 'Pink' is returned to indicate the labelling
           was a failure.
        */
        private string getColourString(string colourHex)
        {
            /* Seperating the string into seperate RGB values
               string = '#RRGGBB'
            */
            double red = Int32.Parse(colourHex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            double green = Int32.Parse(colourHex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            double blue = Int32.Parse(colourHex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

            /* Getting a percentage of how close each value is to each other
               This is used to help label the colour.
               The values are subtracted from eachother and the result is used to create
               a percentage within the 0-255 RGB range.
               High Number = low similarity
               Low Number = high similarity
            */ 
            double RG = ((System.Math.Abs(red - green)) / 255) * 100;
            double GB = ((System.Math.Abs(green - blue)) / 255) * 100;
            double BR = ((System.Math.Abs(blue - red)) / 255) * 100;

            /*
               Lables: White, Grey, Black
               Using a threshold that indicates how close all RGB values are to eachother,
               allows for the seperation of likely white, grey and black colours.
            */
            int similarThreshold = 10;
            if (RG < similarThreshold && GB < similarThreshold && BR < similarThreshold)
            {
                /*
                   Range: 0-255
                   Where all RGB values fall within the range indicated, the colour is labelled.

                   Black: 0-85 
                   Grey : 85-170
                   White: 170-255
                */
                int lowerThird = 85;
                int middleThird = 170;

                if (red >= middleThird && green >= middleThird && blue >= middleThird)
                {
                    return "White";
                }
                if (red >= lowerThird && red < middleThird && green >= lowerThird 
                    && green < middleThird && blue >= lowerThird && blue < middleThird)
                {
                    return "Grey";
                }
                if (red < lowerThird && green < lowerThird && blue < lowerThird)
                {
                    return "Black";
                }
            }

            /*
               Lables: Blue
               RGThresholdBlue: This is the threshold to find colours where the amount Red 
                                and Green within the colour is similar - to find a blue colour.
               rgThresholdBlue: This is the threshold to find colours with low amounts of green
                                and red - to find a blue colour.
               bThresholdBlue:  This is the threshold that ensures a minimum amount of 
                                blue wihtin the colour.
            */
            int RGThresholdBlue = 20;
            int rgThresholdBlue = 100;
            int bThresholdBlue = 100;
            if (RG > RGThresholdBlue && red < rgThresholdBlue && green < rgThresholdBlue && blue > bThresholdBlue)
            {
                return "Blue";
            }

            /*
               Lables: Red, Green, Yellow
               bThresholdLow:     This is the initial threshold to have colours with only
                                  low amounts of blue - to find red, green, yellow. 
               RGThresholdRG:     This is the threshold to find colours where the amount Red 
                                  and Green within the colour is similar - want a high difference
                                  to seperate red and green.
               RGThresholdYellow: This is the threshold to find colours with low amounts of green
                                  and red - to find a yellow colour.
               bThresholdYellow:  This is the threshold that ensures a minimum amount of 
                                  blue wihtin the colour - to find yellow.
            */
            int bThresholdLow = 150;
            int RGThresholdRG = 70;
            int RGThresholdYellow = 5;
            int bThresholdYellow = 50;
            if (blue < bThresholdLow)
            {
                if (RG > RGThresholdRG)
                {
                    if (red > green)
                        return "Red";
                    if (green > red)
                        return "Green";
                }
                else if (RG < RGThresholdYellow && blue < bThresholdYellow)
                {
                    return "Yellow";
                }
            }

            /*
               Label: Pink
               Meaning the colour was not accurately labelled.
            */
            return "Pink";
        }

        /*
            getRGBHexValue - returns string "00"
            Returns a two digit string that represents a hexidecimal number - to represent a colour RGB value.
        */

        private string getRGBHexValue()
        {
            Random random = new Random();
            var hex = random.Next(255).ToString("X");
            if (hex.Length < 2)
                hex = "0" + hex;

            return hex;
        }

        /*
            getColour - API Request
            A RGB value is created and labelled until the colour is labelled correctly - not pink.
            Once a rgb (#001122) and label are created the values are sent out with the HTTPGet request.
        */
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
