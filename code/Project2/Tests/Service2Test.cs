using System;
using Xunit;
using Service2;
using Service2.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    public class Service2Test
    {
        [Fact]
        public void FruitControllerTest()
        {
            FruitController tester = new FruitController();
            ActionResult<string> fruit = tester.getFruit();

            Assert.NotNull(fruit);
            Assert.IsType<ActionResult<string>>(fruit);
        }
    }
}
