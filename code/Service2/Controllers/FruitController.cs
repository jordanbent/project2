using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.CodeAnalysis;

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
                                    "Mandarin", "Melon", "Nectarine", "Olive", "Oranges",
                                    "Papaya", "Peach", "Pomegranate", "Pineapple", "Passion Fruit",
                                    "Strawberries", "Watermelon"};
        private int getIndex()
        {
            Random random = new Random();
            return random.Next(fruits.Length);
        }

        private string getFruit(int index)
        {
            return fruits[index];
        }

        [HttpGet]
        public ActionResult<string> getFruit()
        {
            int index = getIndex();
            return getFruit(index);
        }
    }
}
