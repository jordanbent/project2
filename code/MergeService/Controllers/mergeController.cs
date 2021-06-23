﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            with a description of the user based on the colour given to them. 
        */
        private string colourLogic(string colourName)
        {
            switch (colourName)
            {
                case "White":
                    return "You are hopeful and innocent, ";
                case "Grey":
                    return "You are practical and mature, ";
                case "Black":
                    return "You are powerful and mysterious, ";
                case "Blue":
                    return "You are honest and loyal, ";
                case "Red":
                    return "You are passionate and dramatic, ";
                case "Green":
                    return "You are reliable and nourishing, ";
                case "Yellow":
                    return "You are optimistic and enthusiactic, ";
                default:
                    return "";
            }
        }

        /*
            fruitLogic - takes in fruit name as a string and returns a corresponding string
            with a prediction of the users future, based on the fruit given to them. 
        */
        private string fruitLogic(string fruit)
        {
            switch (fruit)
            {
                case "Apples":
                    return "and you will soon be tempted by something forbidden.";
                case "Apricots":
                    return "and you will soon have plenty positivity in your life.";
                case "Bananas":
                    return "and have a blossoming love life ahead.";
                case "Blueberries":
                    return "and will soon be weakened to only recover speedily.";
                case "Cherries":
                    return "and you will live a long and healthy life.";
                case "Cucumbers":
                    return "and you will soon be able to redeem yourself from any wrong doings.";
                case "Dates":
                    return "and you're lide will soon be filled with abundance.";
                case "Dragon Fruit":
                    return "and you will soon have a fulfilling victory.";
                case "Elderberry":
                    return "and you will be visited by sorrow soon.";
                case "Fig":
                    return "and you will soon be enlightened.";
                case "Grapefruit":
                    return "and you will soon become overcome with jealousy.";
                case "Grapes":
                    return "and you will soon have a joyful week of excess.";
                case "Gooseberries":
                    return "and you will soon become deluded.";
                case "Guava":
                    return "and soon you will have a great life opportunity presented to you.";
                case "Jackfruit":
                    return "and soon all of the hardwork you have put into your passion will pay off.";
                case "Kiwi":
                    return "and you will soon be gifted generously with charity.";
                case "Kumquat":
                    return "and your home will soon be gifted with prosperity.";
                case "Lime":
                    return "and you will soon see your heart filled.";
                case "Lychee":
                    return "and you will see a fulfilling and fun Summer.";
                case "Mango":
                    return "and you will soon see your most desired wishes come true.";
                case "Orange":
                    return "and you will soon be showered in luxuries.";
                case "Melon":
                    return "and you will soon be graced with great health and vitality.";
                case "Olive":
                    return "and you will soon be at peace within your life.";
                case "Papaya":
                    return "and you will soon see your life cleansed and nourished.";
                case "Peach":
                    return "and you will live a long life.";
                case "Pomegranate":
                    return "and you will soon gain more responsibilties in your life.";
                case "Pineapple":
                    return "and you will soon be welcomed with warm arms and celebrated.";
                case "Passion Fruit":
                    return "and you will soon have to make a sacrafice.";
                case "Strawberries":
                    return "and you will soon become enamoured in love.";
                case "Watermelon":
                    return "and you will soon have a long desired time of rest.";
                default:
                    return "";
            }
        }

        /*
            Get - HTTPGet Request; using configuration settings of the mergeController, the 
            colourServiceURL and fruitServiceURL are obtained (stored in appsettings.json locally, 
            and stored in application settings on Azure Web Apps), each API, colourService and fruitService, 
            is called and the data is returned.
            Using the two above functions, the logic of the mergeController is applied and concatonated. This
            string is then sent in the HTTP request.  
        */
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colourService = $"{Configuration["colourServiceURL"]}/colour";
            var colour = "";
            var colourName = "";
            var serviceOneResponseCall = await new HttpClient().GetStringAsync(colourService);

            var colourResponse = serviceOneResponseCall.Split("/");
            colour = colourResponse[0];
            colourName = colourResponse[1];
            var cLogic = colourLogic(colourName);

            var fruitService = $"{Configuration["fruitServiceURL"]}/fruit";
            var serviceTwoResponseCall = await new HttpClient().GetStringAsync(fruitService);
            var fLogic = fruitLogic(serviceTwoResponseCall);

            var mergedResponse = $"{colour}\n{colourName}\n{cLogic}\n{serviceTwoResponseCall}\n{fLogic}";
            return Ok(mergedResponse);
        }
    }
}