using Marg_UserInterface.Models;
using Marg_UserInterface.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Marg_UserInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserManager userManager;

        public HomeController(ILogger<HomeController> logger, IUserManager userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            //userManager.Add();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //public string Index()
        //{
        //    return "home/index";
        //}
        //localhost:5000/home/about
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Personeller()
        {
            return View("Personel");
        }
        public IActionResult Dokuman()
        {
            return View("Dokuman");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
