using System;
using Xunit;
using Service1;
using Service1.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    public class Service1Test
    {
        [Fact]
        public void ColourControllerTest()
        {
            ColourController tester = new ColourController();
            ActionResult<string> colour = tester.getColour();

            Assert.NotNull(colour);
        }
    }
}
