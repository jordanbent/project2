﻿using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }

        public async Task<IActionResult> Index()
        {
            var mergedService = $"{configuration["mergeService"]}/merge";
            var serviceThreeResponseCall = await new HttpClient().GetStringAsync(mergedService);
            var response = serviceThreeResponseCall.Split("\n");

            var colours = response[0].Split("/");
            ViewBag.colour = colours[0];
            ViewBag.colourName = colours[1];
            ViewBag.fruit = response[1];
            return View();
        }
    }
}
