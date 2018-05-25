using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.MySqlConnect.Models;

namespace Test.MySqlConnect.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            using (var context = new DataContext())
            {
                context.Database.EnsureCreated();
                var user = new User { Name = "愤怒的TryCatch" };
                context.Add(user);

                context.SaveChanges();
            }


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
