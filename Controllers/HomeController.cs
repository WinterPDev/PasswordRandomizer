using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordRandomizer.Models;
using Microsoft.AspNetCore.Http;

namespace PasswordRandomizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("passcode") == null)
            {
                HttpContext.Session.SetString("passcode", "Click Generate to Start!");
            }
            if(HttpContext.Session.GetInt32("rolls") == null)
            {
                HttpContext.Session.SetInt32("rolls",0);
            }

            ViewBag.Passcode = HttpContext.Session.GetString("passcode");
            ViewBag.Rolls = HttpContext.Session.GetInt32("rolls");

            return View();
        }

        [HttpGet]
        [Route("result")]
        public IActionResult GetPW()
        {
            int? rolls = HttpContext.Session.GetInt32("rolls");
            rolls++;

            HttpContext.Session.SetInt32("rolls", (int)rolls);
            HttpContext.Session.GetString("passcode");
            HttpContext.Session.SetString("passcode", GeneratePassword(14));
            return RedirectToAction("Index");
        }

        public string GeneratePassword(int num)
        {
            Random rand = new Random();
            string pw = "";
            string keys = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            for(int i = 0; i < num; i++){
                pw += keys[rand.Next(keys.Length)];
            }
            return pw;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
