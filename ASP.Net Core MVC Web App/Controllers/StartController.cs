using ASP.Net_Core_MVC_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_Core_MVC_Web_App.Controllers
{

    public class StartController : Controller
    {

        public string Hello()
        {
            return GetDayTime();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public string GetDayTime()
        {
            var currentTime = DateTime.Now.Hour;
            var greeting = "";
            if (currentTime >= 0 && currentTime < 6)
                greeting = "Доброй ночи";
            else if (currentTime >= 6 && currentTime < 12)
                greeting = "Доброе утро";
            else if (currentTime >= 12 && currentTime < 18)
                greeting = "Добрый день";
            else greeting = "Добрый вечер";
            return greeting;
        }
    }
}
