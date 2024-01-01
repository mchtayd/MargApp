using Marg_UserInterface.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marg_UserInterface.Controllers
{
    public class ProducController:Controller
    {
        public string Index()
        {
            return "produc/index";
        }

        //localhost:5000/produc/list
        public IActionResult List()
        {
            List<Login> logins = new List<Login>();

            Login login = new Login("123", "Abc");
            Login login2 = new Login("550", "Abc");

            logins.Add(login);
            logins.Add(login2);

            return View(logins);
        }
        //localhost:5000/produc/details
        public IActionResult Details()
        {
            return View();
        }
    }
}
