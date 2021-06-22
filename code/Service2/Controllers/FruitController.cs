using Microsoft.AspNetCore.Mvc;
using System;

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
        
        /*
            getIndex - generates random number between 0 and the max length of the array.
        */
        private int getIndex()
        {
            Random random = new Random();
            return random.Next(fruits.Length);
        }

        /*
            getFruit - using a given index, the array is accessed to produce the random fruit.
        */
        private string getFruit(int index)
        {
            return fruits[index];
        }

        /*
            getFruit - HTTPGet request; generates a random index and then fetches the random fruit.
        */
        [HttpGet]
        public ActionResult<string> getFruit()
        {
            int index = getIndex();
            return getFruit(index);
        }
    }
}
