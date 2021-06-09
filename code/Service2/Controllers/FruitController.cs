using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Service2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FruitController : ControllerBase
    {
        private string[] fruits = { "Apples", "Apricots", "Bananas", "Blueberries",
                                    "Cherries", "Cucumbers", "Dates", "Dragon Fruit",
                                    "Elderberry", "Fig", "Grapefruit", "Grapes", "Gooseberries", "Guava",
                                    "Jackfruit", "Kiwi", "Kumquat", "Lime", "Lychee", "Mango",
                                    "Mandarin Orange", "Melon", "Nectarine", "Olive", "Oranges",
                                    "Papaya", "Peach", "Pomegranate", "Pineapple", "Passion Fruit",
                                    "Strawberries", "Watermelon"};
        private int getIndex()
        {
            Random random = new Random();
            return random.Next(fruits.Length);
        }


        [HttpGet]
        public ActionResult<string> getFruit()
        {
            return fruits[getIndex()];
        }
    }
}
