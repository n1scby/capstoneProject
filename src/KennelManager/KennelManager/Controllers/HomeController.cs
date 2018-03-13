using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KennelManager.Models;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace KennelManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDogRepository _dogRepo;
        private readonly ILogger _logger;

        public HomeController(IDogRepository dogRepo, ILogger<HomeController> logger)
        {
            _dogRepo = dogRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
