using System;
using Xunit;
using MergeService;
using MergeService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    public class MergeTest
    {
        private AppSettings appSettings = new AppSettings()
        {
            colourService = "https://testservice1-jb.azurewebsites.net",
            fruitService = "https://testservice2-jb.azurewebsites.net"
        };

        private Mock<IOptions<AppSettings>> mockConfig;

        [Fact]
        public void MergeControllerTest()
        {
            mockConfig = new Mock<IOptions<AppSettings>>();
            mockConfig.Setup(x => x.Value).Returns(appSettings);

            MergeController tester = new MergeController(mockConfig.Object);
            Task<IActionResult> merge = tester.Get();

            Assert.NotNull(merge);
            Assert.IsType<OkObjectResult>(merge);
            
        }
    }
}
