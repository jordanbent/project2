using System;
using Xunit;
using FrontEnd;
using FrontEnd.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    public class FrontEndTest
    {
        private Mock<IConfiguration> mockConfig;
        private Mock<ILogger<HomeController>> logger;

        [Fact]
        public void MergeControllerTest()
        {
            mockConfig = new Mock<IConfiguration>();
            logger = new Mock<ILogger<HomeController>>();

            HomeController tester = new HomeController(logger.Object, mockConfig.Object);
            Task<IActionResult> frontEnd = tester.Index();
            
            Assert.NotNull(frontEnd);
            Assert.IsType<Task<IActionResult>>(frontEnd);
        }
    }
}
