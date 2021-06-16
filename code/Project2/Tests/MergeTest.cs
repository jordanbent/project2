using System;
using Xunit;
using MergeService;
using MergeService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;

namespace Tests
{
    public class MergeTest
    {
        private Mock<IConfiguration> mockConfig;

        [Fact]
        public void MergeControllerTest()
        {
            mockConfig = new Mock<IConfiguration>();

            MergeController tester = new MergeController(mockConfig.Object);
            Task<IActionResult> merge;

            for (int i = 0; i < 100; i++)
            {
                merge = tester.Get();

                Assert.NotNull(merge);
                Assert.IsType<Task<IActionResult>>(merge);
            }
        }
    }
}
