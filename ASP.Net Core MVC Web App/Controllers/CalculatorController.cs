using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_Core_MVC_Web_App.Controllers
{
    public class CalculatorController : Controller
    {
        public double Index(double a, double b)
        {
            return a + b;
        }
    }
}
