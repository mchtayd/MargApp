using Marg_UserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marg_UserInterface.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            Login login = new Login(25, "0113", "1");

            //ViewBag
            //Model
            //ViewData

            //ViewBag.Degisken = login.Sicil;
            ViewData["Login"] = login;
            ViewData["Personeller"] = "Mücahit";

            ViewBag.login = login;

            return View(login);
        }

    }
}
